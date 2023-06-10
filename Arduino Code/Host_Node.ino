/*
 * Host Node code for ECCE 44x Senior Capstone Project 2022-23
 * By Garren Dutto, Blake Garcia, Emma Dacus, and Grace Mackey
 * Oregon State University
 * 
 * Module uses an Arduino Nano Every with the RFM95 breakout board
 * from Adafruit, connected to a computer with the appropriate data
 * processing code so that sensor data can be uploaded to the website
 * 
 * Connections:
 * D0 - 
 * D1 - 
 * D2 - RFM95 Pin G0 (Interrupt Request)
 * D3 -
 * D4 - 
 * D5 - 
 * D6 -
 * D7 -
 * D8 - 
 * D9 - RFM95 Reset
 * D10 - RFM95 CS
 * D11 - MOSI
 * D12 - MISO
 * D13 - SCK
 */

 #include <SPI.h>
 #include <RH_RF95.h>

 #define RFM95_CS 10 //CS pin for RFM95
 #define RFM95_RST 9 //Reset pin
 #define RFM95_INT 2 //Interrupt pin, MUST be pin 2

 #define RF95_FREQ 923.0 //Operating frequency

 //Singleton instance of the radio driver
 RH_RF95 rf95(RFM95_CS, RFM95_INT);

//Address catalog
uint8_t addresses[16];
int num_addresses;

uint8_t messages[20];
int message_index;

typedef struct{
  //uint8_t identifier;
  float temperature;
  float humidity;
  uint16_t water_level;
  uint16_t wind_speed;
  uint16_t cal_temp;
  float wind_speed_kph;
}sensor_data;

sensor_data readings[16];

const float zeroWindAdjustment =  -0.5; // negative numbers yield smaller wind speeds and vice versa.

unsigned long lastmillis;

unsigned long last_received[16]; //Last received milliseconds for each node


/*****************************************************************************
 * Function: address_request
 * Description: Handles an address request message by finding the first 
 * available address in the list of addresses, and then sending it back 
 * with the new address as the destination.
 * Parameters: The received message and its length
 * Return type: none
 ****************************************************************************/
void address_request(uint8_t *buf, uint8_t len){

  /*First: Check to see if this message has been received already*/
  bool received = false;
  for(int i = 0; i < 10; i++){
    if(messages[i] == buf[1]){
      received = true;
    }
  }

  /*If the message has been received, drop it*/
  if(received){
    return;
  }

  /*If not, add it to the message array and send a reply*/
  messages[message_index] = buf[1];
  message_index++;
  message_index = message_index % 10;

  /*Generate a new address, using the first available from the list*/
  uint8_t addr = 1;
  bool done = true;
  
  for(int i = 0; i < num_addresses; i++){
    if(addr == addresses[i]){
      addr++;
      i = 0;
    }
  }

  addresses[num_addresses] = addr;
  num_addresses++;

  /*Replace the source address with 0, and the destination with the new address*/
  buf[1] = buf[1] + 1;
  buf[2] = 0;
  buf[3] = addr;

  rf95.send(buf, len);
  rf95.waitPacketSent();

  /*Printing for network testing*/
  /*
  Serial.print(F("Address assigned on message "));
  Serial.print(char (buf[1]));
  Serial.print(F(" is "));
  Serial.println(addr);*/
  
}

/*************************************************************************************************
 * Function: read_sensor_data
 * Description: Reads in the sensor data contained in a message from one of the sensor nodes. 
 * Parses through the data and passes it to the website as necessary.
 * Parameters: uint8_t array that is the received message, and the length of the message
 ************************************************************************************************/
 void read_sensor_data(uint8_t *buf, uint8_t len){

  //Serial.println(F("Received test sensor packet"));
  //readings[buf[2]].identifier = buf[2];

  /*Check if the sender is in the address list, and if it isn't, add it to the list*/
  uint8_t sender = buf[2];
  for(int i = 0; i < num_addresses; i++){
    if(addresses[i] == sender){
      break;
    }else if(i == num_addresses - 1){
      addresses[num_addresses] = sender;
      num_addresses++;
      break;
    }
  }

  if(num_addresses == 0){
    addresses[num_addresses] = sender;
    num_addresses++;
  }

  memcpy(&readings[buf[2]], buf + 4, 14);

  int TMP_Therm_ADunits;
  float RV_Wind_ADunits;
  float RV_Wind_Volts;
  int TempCtimes100;
  float zeroWind_ADunits;
  float zeroWind_volts;
  float WindSpeed_MPH;
  
  TMP_Therm_ADunits = readings[buf[2]].wind_speed;
  RV_Wind_ADunits = readings[buf[2]].cal_temp;
  RV_Wind_Volts = (RV_Wind_ADunits * 0.0048828125);

  TempCtimes100 = (0.005 *((float)TMP_Therm_ADunits * (float)TMP_Therm_ADunits)) - (16.862 * (float)TMP_Therm_ADunits) + 9075.4;  

  zeroWind_ADunits = -0.0006*((float)TMP_Therm_ADunits * (float)TMP_Therm_ADunits) + 1.0727 * (float)TMP_Therm_ADunits + 47.172;  //  13.0C  553  482.39

  zeroWind_volts = (zeroWind_ADunits * 0.0048828125) - zeroWindAdjustment;
  WindSpeed_MPH =  pow(((RV_Wind_Volts - zeroWind_volts) /.2300) , 2.7265);
  readings[buf[2]].wind_speed_kph = WindSpeed_MPH * 1.609; 

  sender = buf[2];
  buf[0] = '3'; //ACK message indicator
  buf[1] = buf[1] + 1;
  buf[2] = 0;
  buf[3] = sender;
  buf[4] = 'A';
  buf[5] = 'C';
  buf[6] = 'K';
  uint8_t s = 7;
  rf95.send(buf, s);
  rf95.waitPacketSent();
  

  /*The following prints are for debugging purposes only*/
  /*
  Serial.print(F("TEST: "));
  Serial.print(F("Node:"));
  Serial.print(sender);
  Serial.print(",");
  Serial.print(F("Temp:"));
  Serial.print(readings[sender].temperature);
  Serial.print(",");
  Serial.print(F("Humidity:"));
  Serial.print(readings[sender].humidity);
  Serial.print(",");
  Serial.print(F("Wind_Speed:"));
  Serial.print(readings[sender].wind_speed_kph);
  Serial.print(",");
  Serial.print(F("Water_Level:"));
  Serial.print(readings[sender].water_level);
  Serial.print(",");
  Serial.print("Packet_No:");
  Serial.println(buf[1]);*/

  
  
}

void setup() {
  // put your setup code here, to run once:
  randomSeed(analogRead(7));
  pinMode(RFM95_RST, OUTPUT);
  digitalWrite(RFM95_RST, HIGH);

  for(int i = 0; i < 20; i++){
    messages[i] = 0;
  }
  message_index = 0;

  num_addresses = 0;

  lastmillis = 0;

  for(int i = 0; i < 16; i++){
    //readings[i].identifier = 0;
    readings[i].temperature = 0;
    readings[i].humidity = 0;
    readings[i].wind_speed = 0;
    readings[i].water_level = 0;
  }

  //Initialize Serial monitor
  while(!Serial);
  Serial.begin(9600);
  delay(100);

  //Reset the radio
  digitalWrite(RFM95_RST, LOW);
  delay(10);
  digitalWrite(RFM95_RST, HIGH);
  delay(10);

  //Call the radio initialization routine
  while(!rf95.init()){
    Serial.println("LoRa radio init failed");
    while(1);
  }
  //Serial.println("LoRa radio initialized");

  //Set the freqency
  if(!rf95.setFrequency(RF95_FREQ)){
    Serial.println("Failed to set frequency");
    while(1);
  }
  //Serial.println("Set Frequency success");

  //Set transmitting power, minimum 5 and max 23 dBm
  rf95.setTxPower(23, false);
  
}

void loop() {
  // put your main code here, to run repeatedly:

  //Check to see if a message is available
  if(rf95.waitAvailableTimeout(1000)){

    uint8_t buf[RH_RF95_MAX_MESSAGE_LEN];
    uint8_t len = sizeof(buf);

    //read in the message
    if(rf95.recv(buf, &len)){
      //Read the first byte, if it is a 0, then send an available address and the path
      //If it is a 1, read in the data and process it
      //If it is a 2, notify the website
      switch(buf[0]){
        case '0':
          //Find the first available address and attach it into the message,
          //Serial.println(F("Handling address request"));
          address_request(buf, len);
          break;
        case '1':
          //Read in the data and parse through it
          read_sensor_data(buf, len);
          break;
        case '2':
          //Read the error and report to the website
          break;
        default:
         //Something must have gone wrong with the packet, so drop it
         break;
      }
    }else{
      //Something went wrong reading in the message
    }
  }

  /* On certain intervals, send data to the website, and then send out warning messages
   *  to any addresses that did not send any data in the past 30 minutes*/

  /*If 15 minutes have elapsed, send the data by printing to the serial port*/
  if(millis() - lastmillis >= 900000){

    lastmillis = millis();
    for(int i = 0; i < num_addresses; i++){

      /*Iterate through the addresses and send the sensor data for each one*/
     
      Serial.print(F("READING:   "));
      Serial.print(F("Node:"));
      Serial.print(addresses[i]);
      Serial.print(F(",")); 
      Serial.print(F("Temp:"));
      Serial.print(readings[addresses[i]].temperature);
      Serial.print(",");
      Serial.print(F("Humidity:"));
      Serial.print(readings[addresses[i]].humidity);
      Serial.print(",");
      Serial.print(F("Wind_Speed:"));
      Serial.print(readings[addresses[i]].wind_speed_kph);
      Serial.print(",");
      Serial.print(F("Water_Level:"));
      Serial.print(readings[addresses[i]].water_level);
      Serial.println();

    }   
    
    /* Remove any nodes from the address list that did not send data within the 15
      minute time frame*/
    for(int i = 0; i < num_addresses; i++){

      /*If one of the addresses did not transmit, move it to the end of the array and then
        decrement the address number index*/ 
      if(readings[addresses[i]].temperature == 0 && readings[addresses[i]].humidity == 0){
        for(int j = i; j < num_addresses-1; j++){
          uint8_t temp = addresses[j+1];
          addresses[j+1] = addresses[j];
          addresses[j] = temp;
        }
        num_addresses--;
      }
    } 

    /*Set all values in the readings array to 0*/
    for(int i = 0; i < num_addresses; i++){

      readings[addresses[i]].temperature = 0;
      readings[addresses[i]].humidity = 0;
      readings[addresses[i]].wind_speed_kph = 0;
      readings[addresses[i]].water_level = 0;

    }

  /*At 14 minutes, send out a warning to any nodes that have not transmitted data yet*/
  /*Message code: 2*/
  }else if(millis() - lastmillis >= 840000){

    for(int i = 0; i < num_addresses; i++){
      /*If the sensor readings are all 0, then the node has not sent any data*/
      if(readings[addresses[i]].temperature == 0 && readings[addresses[i]].humidity == 0){
        uint8_t buf[RH_RF95_MAX_MESSAGE_LEN];
        uint8_t len = sizeof(buf);
        /*Create a packet with code '2' with the destination as the target node*/
        uint8_t message[4] = {50, 0, 0, addresses[i]};
        rf95.send(message, 4);
        rf95.waitPacketSent();

        /* Wait for a response for 7 seconds*/  
        if(rf95.waitAvailableTimeout(7000)){
           if(rf95.recv(buf, &len)){
             if(buf[0] == '2' && buf[2] == addresses[i]){
               
               memcpy(&readings[buf[2]], buf + 4, 14);

               int TMP_Therm_ADunits;
               float RV_Wind_ADunits; float RV_Wind_Volts;
               int TempCtimes100;
               float zeroWind_ADunits;
               float zeroWind_volts;
               float WindSpeed_MPH;
  
               TMP_Therm_ADunits = readings[buf[2]].wind_speed;
               RV_Wind_ADunits = readings[buf[2]].cal_temp;
               RV_Wind_Volts = (RV_Wind_ADunits * 0.0048828125);

               TempCtimes100 = (0.005 *((float)TMP_Therm_ADunits * (float)TMP_Therm_ADunits)) - (16.862 * (float)TMP_Therm_ADunits) + 9075.4;  

               zeroWind_ADunits = -0.0006*((float)TMP_Therm_ADunits * (float)TMP_Therm_ADunits) + 1.0727 * (float)TMP_Therm_ADunits + 47.172;  //  13.0C  553  482.39

               zeroWind_volts = (zeroWind_ADunits * 0.0048828125) - zeroWindAdjustment;
               WindSpeed_MPH =  pow(((RV_Wind_Volts - zeroWind_volts) /.2300) , 2.7265);

               readings[buf[2]].wind_speed_kph = WindSpeed_MPH * 1.609; 
             }  
           }
        }
        
      }
    }
  }

   delay(1);
   
}

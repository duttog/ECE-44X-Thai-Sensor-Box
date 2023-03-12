/*
 * Transmitter Code for ECE 44x Senior Capstone Project 2022-23
 * By Garren Dutto, Blake Garcia, Emma Dacus, and Grace Mackey.
 * 
 * Module uses an Arduino Nano Every with the RFM95 breakout board
 * from Adafruit, 
 * (TODO: INSERT SENSOR PARTS TO COMMENT)
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
 * D8 - BME280 SS
 * D9 - RFM95 Reset
 * D10 - RFM95 SS
 * D11 - MOSI
 * D12 - MISO
 * D13 - SCK
 * 
 * A0 - Wind Sensor Analog Read Pin
 * A1 - Water Level Sensor Analog Read Pin
 * A2 - 
 * A3 - 
 * A4 - 
 * A5 - 
 * A6 - 
 * A7 - RFM95 Random Number
 */

 #include <SPI.h>
 #include <RH_RF95.h>
 #include <Adafruit_Sensor.h>
 #include <Adafruit_BME280.h>

 #define RFM95_CS 10 //SS pin for RFM95
 #define RFM95_RST 9
 #define RFM95_INT 2
 #define BME_CS 8

 #define RF95_FREQ 923.0

 #define PACKET_BYTES 18

//Singleton instance of the radio driver
 RH_RF95 rf95(RFM95_CS, RFM95_INT);

// Hardware SPI bme sensor object
Adafruit_BME280 bme(BME_CS); // hardware SPI

uint8_t address; //Node's address

uint8_t addr_list[16]; //list of nodes connected to this one
int num_addr; //Number of nodes connected to this one

uint8_t messages[10]; //List of previous 10 received message identifiers
int message_index;

uint8_t sensor_data_packet[PACKET_BYTES];

uint8_t msg_id; //Message identifier

bool acknowledged;

/** This is the data structure holding the sensor data itself **/
typedef struct {
  float temp;
  float humidity;
  uint16_t water_level;
  uint16_t wind_speed;
  uint16_t cal_temp;
} sensor_data;

sensor_data sensors = {
  .temp = 0,
  .humidity = 0,
  .water_level = 0,
  .wind_speed = 0,
  .cal_temp = 0,
};




/*******************************************************************************
 * Function: address_request
 * Description: Handles an address request from another node by checking the 
 * contents and whether this node has already seen the request, then appending
 * its address if it has not.
 * Parameters: array of bytes that contains the address request message
 * Return value: none
 *******************************************************************************/
 void address_request(uint8_t buf[], uint8_t len){

  //Two cases: from the base station and from another node
  //Determine the source by finding the "S:" part of the transmission
  int i;

  //Pull out the sender's address
  uint8_t sender = 0;
  sender = buf[2];

  /*Check the message identifier, and throw out the message if the identifier
   * has already been encountered*/
  for(i = 0; i < 10; i++){
    if(messages[i] == buf[1]){
      return;
    }
  }

  //If it has not been encounter, add it to the messages array
  messages[message_index] = buf[1];
  message_index++;
  message_index = message_index % 10;

  /*
   * FIRST: check the address list to see if this node is on it
   */

  bool path = false;
  int temp = 0;
  i = 5;
  //Check each address in the path
  //While the end of the array has not been reached, check each 4-character address
  while(i != len - 1){
    //each character is an address
    temp = buf[i];

    //Check to see if this node's address matches
    if(temp == address){
      path = true;
      break;
    }
    i++;
  }

  //If the base station was the one that sent the message, rebroadcast
  if(sender == 0 && path){
    //If this node was on the address list, then rebroadcast the message
    //Also add the new address to this node's address list
    addr_list[num_addr] = temp;
    num_addr++;
    rf95.send(buf, len);
    rf95.waitPacketSent();
    
  }else{ //If the sender is another node
    //If this node's address is not in the address list, add it to the list and retransmit
    if(!path){
      buf[len] = address;
      len++;

      rf95.send(buf, len);
      rf95.waitPacketSent();
    }
  }

}


/*****************************************************************************
 * Function: sensor_rebroadcast
 * Description: Parses through a message that contains sensor data and 
 * rebroadcasts the message if the message is on this node's address list and
 * the message has not been received already
 * Parameters: A byte array that is the message, and the length of the message
 * Return type: none
 *****************************************************************************/
 void sensor_rebroadcast(uint8_t *buf, uint8_t len){
  /*Find the sender's address*/
  uint8_t sender = buf[2];
  uint8_t dest = buf[3];

  if(sender == 0 && dest == address){
    acknowledged = true;
    return;
  }

  /*If the sender is on the address list, rebroadcast*/
  bool sender_path = false;
  bool dest_path = false;
  for(int i = 0; i < 256; i++){
    if(addr_list[i] == sender){
      sender_path = true;
    }else if(addr_list[i] == dest){
      dest_path = true;
    }
  }

  if(sender_path){
    rf95.send(buf, len);
    rf95.waitPacketSent();
  }else if(dest_path){
    rf95.send(buf, len);
    rf95.waitPacketSent();
  }
  
}

/******************************************************************************
 * Function: error_message
 * Description: Handles an error message.  If the message is from one of the 
 * other nodes, broadcast it if it hasn't been seen before.  If it is from the
 * source node, broadcast if this node is not the destination, otherwise send
 * a reply
 ******************************************************************************/
 void error_message(uint8_t *buf, uint8_t len){
  /*Pull out the source and destination*/
  uint8_t source = buf[2];
  uint8_t dest = buf[3];

  /*If this node has seen the message before, drop it*/
  for(int i = 0; i < 10; i++){
    if(buf[1] == messages[i]){
      return;
    }
  }

  /*Add the message ID to the list of seen messages*/
  messages[message_index] = buf[1];
  message_index++;
  message_index = message_index % 10;

  /*If the source is one of the other nodes, broadcast the error message*/
  if(source != 0 && dest != address){
    rf95.send(buf, len);
    rf95.waitPacketSent();
    }else{
     
  }
}


/******************************************************************************
 * Function: read_sensors
 * Description: Reads all four data values from the sensors into the global
 * variable sensor_data
 * Parameters: None
 * Return type: None
 ******************************************************************************/
void read_sensors(){
  // global variable holding sensor data is named "sensors"

  // start by initializing the bme280
  
  bme.begin();
  

  // read the temperature and humidity from the BME280
  sensors.temp = bme.readTemperature();
  sensors.humidity = bme.readHumidity();
  
  // read the analog pins directly
  sensors.wind_speed = analogRead(A1);
  sensors.cal_temp = analogRead(A0);
  sensors.water_level = analogRead(A2);
}


/******************************************************************************
 * Function: create_packet
 * Description: generates a sensor data packet to send to the host node
 * Parameters: None
 * Return type: None
 ******************************************************************************/
void create_packet(){
  // name of packet data is "sensor_data_packet"

  msg_id++;
  sensor_data_packet[0] = '1';
  sensor_data_packet[1] = msg_id;
  sensor_data_packet[2] = address;
  sensor_data_packet[3] = 0; // sending message data to the host node

  memcpy(sensor_data_packet + 4, &sensors, 14);
}

/******************************************************************************
 * Function: setup
 * Description: Initializes the radios and sends out an address request
 * Parameters: None
 * Return type: None
 ******************************************************************************/
void setup() {
  // put your setup code here, to run once:
  randomSeed(analogRead(7));
  pinMode(RFM95_RST, OUTPUT);
  digitalWrite(RFM95_RST, HIGH);

  /*IMPORTANT: DO NOT CHANGE THIS OR IT WILL FRY A PIN*/
  pinMode(4, OUTPUT);
  digitalWrite(4, LOW);

  /*Ok, you can change stuff after here*/

  num_addr = 0;
  message_index = 0;
  acknowledged = true;

  //Initialize Serial Monitor
  while (!Serial);
  Serial.begin(9600);
  delay(100);

  //Reset the radio
  digitalWrite(RFM95_RST, LOW);
  delay(10);
  digitalWrite(RFM95_RST, HIGH);
  delay(10);

  //Call the initialization routine
  while (!rf95.init()){
    Serial.println(F("LoRa radio init failed"));
    while(1);
  }
  Serial.println(F("Lora radio initialized"));

  //Set the frequency to the specified value
  if(!rf95.setFrequency(RF95_FREQ)){
    Serial.println(F("Failed to set frequency"));
    while(1);
  }
  Serial.println(F("Set Freqency success"));

  //Set transmitting power, can be anywhere from 5 to 23 dBm
  rf95.setTxPower(23, false);

  delay(100);

  //Send out address request
  address = 0;
  while(address == 0){
    msg_id = random(0, 255); //Random byte identifier
    uint8_t msg[4] = {48, msg_id, 255, 0}; //0 identifier is for address request, 1 is for sensor reading, 2 is for error 
    Serial.print(F("Address request message: "));
    Serial.print((char*)msg);
    rf95.send(msg, sizeof(msg));
    rf95.waitPacketSent();

     //wait 5 seconds for a response
    if(rf95.waitAvailableTimeout(5000)){
      //The structure of the message should be "0xSD" with D being the new address in hex
      uint8_t buf[RH_RF95_MAX_MESSAGE_LEN];
      uint8_t len = sizeof(buf);

      //Make sure that the message is received properly
      if(rf95.recv(buf, &len) && buf[1] == msg_id){
        address = buf[3];
      }
    }

    /*If no address reply was received, the node will loop every 5 seconds and send a new
     * address request until it gets a reply.
     */

    /* BME280 Temp / Humidity Sensor Init */
    if (! bme.begin()) {
        Serial.println(F("Could not find a valid BME280 sensor, check wiring!"));
        while (1);
    }
    
    bme.setSampling(Adafruit_BME280::MODE_FORCED,
                    Adafruit_BME280::SAMPLING_X1,   // temperature
                    Adafruit_BME280::SAMPLING_NONE, // pressure
                    Adafruit_BME280::SAMPLING_X1,   // humidity
                    Adafruit_BME280::FILTER_OFF );
    /*** BME280 Temp / Humidity Sensor Init ***/
    
  }
  
}

void loop() {
  // put your main code here, to run repeatedly:

  //Check to see if a message has been received
  if(rf95.waitAvailableTimeout(5000)){

    uint8_t buf[RH_RF95_MAX_MESSAGE_LEN];
    uint8_t len = sizeof(buf);

    //Read in the message
    if(rf95.recv(buf, &len)){
      //Check the first byte of the transmission. If it is 0, then it is an address request
      //If it is 1, then it is a data transmission. If it is a 2, it is an ACK
      switch(buf[0]){
        case '0': 
          //Append node's address to the message and then broadcast if it is not from base station
          //If it is from base station, check to see if this is the intended recipient
          address_request(buf, len);
          break;
        case '1':
          //Check if the address is in the sender's address list and broadcast the message if it is
          sensor_rebroadcast(buf, len);
          break;
        case '2':
          //Handle error messages
          error_message(buf, len);
          break;
        default:
          //something must have gone wrong with the packet, so drop it
          break;
      }
    }else{
      //Something went wrong reading in the message, honestly idk what to put here
    }
  }


  if(acknowledged == true){
    read_sensors();
    create_packet();
  }

  rf95.send(sensor_data_packet, PACKET_BYTES);
  acknowledged = false;

}

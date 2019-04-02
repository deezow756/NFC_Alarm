#include <LiquidCrystal.h>
#include "SPI.h"
#include "PN532_SPI.h"
#include "snep.h"
#include "NdefMessage.h"
#include "SoftwareSerial.h"

PN532_SPI pn532spi(SPI, 10);
SNEP nfc(pn532spi);
uint8_t ndefBuf[128];
uint8_t recordBuf[128];

LiquidCrystal lcd(7, 8, 9, 10, 11, 12);

void setup()
{    
    Serial.begin(9600);
    lcd.begin(16,2);
    lcd.print("Scan Your Phone");
}

void loop()
{
    Serial.println("Get a message from Android");
    int msgSize = nfc.read(ndefBuf, sizeof(ndefBuf));
    if (msgSize > 0) {
        
        Serial.println("\nSuccess");        
                   
        NdefMessage msg  = NdefMessage(ndefBuf, msgSize);

        NdefRecord record = msg.getRecord(0);
        int payloadLength = record.getPayloadLength();
        uint8_t payload[payloadLength];
        record.getPayload(payload);

        String code = "";

        for(int i = 0; i < payloadLength; i++)
        {
          if(payload[i] == (uint8_t)'30')
          {
            code = code + "1";
          }
          if(payload[i] == (uint8_t)'31')
          {
            code = code + "1";
          }
          if(payload[i] == (uint8_t)'32')
          {
            code = code + "2";
          }
          if(payload[i] == (uint8_t)'33')
          {
            code = code + "3";
          }
          if(payload[i] == (uint8_t)'34')
          {
            code = code + "4";
          }
          if(payload[i] == (uint8_t)'35')
          {
            code = code + "5";
          }
          if(payload[i] == (uint8_t)'36')
          {
            code = code + "6";
          }
          if(payload[i] == (uint8_t)'37')
          {
            code = code + "7";
          }
          if(payload[i] == (uint8_t)'38')
          {
            code = code + "8";
          }
          if(payload[i] == (uint8_t)'39')
          {
            code = code + "9";
          }
        }     

        Serial.println(code);
        lcd.clear();
        lcd.print(code);

        Serial.println("Send a message to Peer");
    
        NdefMessage message = NdefMessage();
        message.addMimeMediaRecord("www.deezow.com", "true");
        //message.addUriRecord("http://arduino.cc");
        //message.addUriRecord("https://github.com/don/NDEF");
            
        int messageSize = message.getEncodedSize();
        if (messageSize > sizeof(ndefBuf)) {
            Serial.println("ndefBuf is too small");
            while (1) {
            }
        }

        message.encode(ndefBuf);
    
        if (0 >= nfc.write(ndefBuf, messageSize)) {
            Serial.println("Failed");
        } else {
            Serial.println("Success");
        }
        delay(10000);
        
    } else {
        Serial.println("failed");
    }
    delay(1000);
}

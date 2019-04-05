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

LiquidCrystal lcd(3, 4, 5, 6, 7, 8);

void setup()
{    
    Serial.begin(9600);
    lcd.begin(16,2);
}

void loop()
{    
    lcd.clear();
    lcd.setCursor(0,0);
    lcd.print("Scan Your Phone");
    int msgSize = nfc.read(ndefBuf, sizeof(ndefBuf));
    if (msgSize > 0) {
                   
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

        lcd.clear();
        lcd.print("Enter Code:");
        lcd.setCursor(0,1);
        lcd.print(code);    
        SendMessage();
        delay(5000);        
    }
    delay(1000);
}

void SendMessage()
{
        NdefMessage message = NdefMessage();
        message.addMimeMediaRecord("www.deezow.com", "true");
            
        int messageSize = message.getEncodedSize();
        message.encode(ndefBuf);
    
        if (0 >= nfc.write(ndefBuf, messageSize)) {
            SendMessage();            
        }
}

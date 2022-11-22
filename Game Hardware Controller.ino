#include <Servo.h> 

int AnalogValue = 0;

int AnalogValue2 = 0;

int switchServo = 0;

Servo myservo;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(3, INPUT);
  pinMode(4, INPUT_PULLUP);
  pinMode(5, INPUT_PULLUP);
  pinMode(6, INPUT);
  pinMode(7, INPUT_PULLUP);
  pinMode(8, INPUT);
  pinMode(9, INPUT);
  pinMode(10, INPUT);
  pinMode(11, INPUT_PULLUP);
  pinMode(12, OUTPUT);
  myservo.attach(2);

  AnalogValue = analogRead(A0);
  AnalogValue2 = analogRead(A1);

}

void loop() {
  // put your main code here, to run repeatedly:

  //Left and Right Analog Sticks
  if(analogRead(A0) > 550)
  {
    Serial.println("LEFT");
    delay(50);
  }
  else if(analogRead(A0) < 520)
  {
    Serial.println("RIGHT");
    delay(50);
  }
  else
  {
    Serial.println("STOP HORIZONTAL");
    delay(50);
  }
  AnalogValue = analogRead(A0);

  //Up and Down Analog Sticks  
  if (AnalogValue2 != analogRead(A1)) {
    //Serial.print("ANALOG Y VALUE: ");
    //Serial.println(analogRead(A1));
    if(analogRead(A1) > 520)
    {
      Serial.println("UP");
      delay(50);
    }
    else if(analogRead(A1) < 490)
    {
      Serial.println("DOWN");
      delay(50);
    }
    else
    {
      Serial.println("STOP VERTICAL");
      delay(50);
    }
    AnalogValue2 = analogRead(A1);
  } else {
  }

  //Circle Button
  if (digitalRead(3) == HIGH) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("CIRCLE");
    digitalWrite(12, HIGH);
    delay(100);
    Serial.println("CIRCLE BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
    digitalWrite(12, LOW);
  }

  //Triangle Button
  if (digitalRead(4) == LOW) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("TRIANGLE");
    digitalWrite(12, HIGH);
    delay(100);
    Serial.println("TRIANGLE BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
    digitalWrite(12, LOW);
  }

  //X Button
  if (digitalRead(5) == LOW) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("X");
    digitalWrite(12, HIGH);
    delay(50);
    Serial.println("X BUFFER");
    delay(50);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
    digitalWrite(12, LOW);
  }

  //Square Button
  if (digitalRead(6) == HIGH) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("SQUARE");
    digitalWrite(12, HIGH);
    delay(100);
    Serial.println("SQUARE BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
    digitalWrite(12, LOW);
  }

  //Right Trigger Button
  if (digitalRead(7) == LOW) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("RIGHT TRIGGER");
    digitalWrite(12, HIGH);
    delay(100);
    Serial.println("RIGHT TRIGGER BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
    digitalWrite(12, LOW);
  }

  //Start Button
  if (digitalRead(9) == HIGH) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("START");

    if(switchServo == 0)
    {
        myservo.write(20);
        switchServo = 1;
        Serial.println("Rotated motor to 20 degrees.");
    }
    else
    {
        myservo.write(170);
        switchServo = 0;
        Serial.println("Rotated motor to 170 degrees.");
    }    
    
    delay(100);
    Serial.println("START BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
  }

  //Select Button
  if (digitalRead(10) == HIGH) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("SELECT");

    if(switchServo == 0)
    {
        myservo.write(20);
        switchServo = 1;
        Serial.println("Rotated motor to 20 degrees.");
    }
    else
    {
        myservo.write(170);
        switchServo = 0;
        Serial.println("Rotated motor to 170 degrees.");
    }  
    
    delay(100);
    Serial.println("SELECT BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
  }

  //Left Trigger Button
  if (digitalRead(11) == LOW) {
    digitalWrite(LED_BUILTIN, HIGH);
    Serial.println("LEFT TRIGGER");
    digitalWrite(12, HIGH);
    delay(100);
    Serial.println("LEFT TRIGGER BUFFER");
    delay(100);
  } else {
    digitalWrite(LED_BUILTIN, LOW);
    digitalWrite(12, LOW);
  }

}

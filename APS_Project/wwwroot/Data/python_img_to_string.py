import base64
  
  
with open("burger.jpg", "rb") as image2string:
    converted_string = base64.b64encode(image2string.read())
  
with open('burger', "wb") as file:
    file.write(converted_string)

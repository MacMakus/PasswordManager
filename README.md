# PasswordManager

**PasswordManager** เป็นไลบรารีสำหรับจัดการการเข้ารหัสและถอดรหัสข้อมูล ที่ช่วยให้คุณสามารถเพิ่มความปลอดภัยให้กับข้อมูลสำคัญ เช่น รหัสผ่าน หรือข้อความที่ต้องการปกปิด(และสามารถขยายได้ในอนาคต)

## ข้อกำหนด
- **Framework**: .NET 6
- **ประเภทโปรเจกต์**: Class Library

### ตัวอย่างการใช้งาน(TrippleDES)
var passwordManager = new PasswordManager();

string key = "MySecretKey123";
string plainText = "Hello, World!";

string encryptedData = passwordManager.EncryptData(key, plainText);
Console.WriteLine($"Encrypted Data: {encryptedData}");

string decryptedData = passwordManager.Decrypt(key, encryptedData);
Console.WriteLine($"Decrypted Data: {decryptedData}");

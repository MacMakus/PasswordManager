# PasswordManager

**PasswordManager** เป็นไลบรารีสำหรับจัดการการเข้ารหัสและถอดรหัสข้อมูล ที่ช่วยให้คุณสามารถเพิ่มความปลอดภัยให้กับข้อมูลสำคัญ เช่น รหัสผ่าน หรือข้อความที่ต้องการปกปิด(และสามารถขยายได้ในอนาคต)

## ข้อกำหนด
- **Framework**: .NET 6
- **ประเภทโปรเจกต์**: Class Library

### ตัวอย่างการใช้งาน(TrippleDES)
```csharp

PasswordManager.TripleDES tripleDes = new PasswordManager.TripleDES();

string key = "MySecretKey123";
string plainText = "Hello, World!";

string encryptedData = tripleDes.EncryptData(key, plainText);
Console.WriteLine($"Encrypted Data: {encryptedData}");

string decryptedData = tripleDes.Decrypt(key, encryptedData);
Console.WriteLine($"Decrypted Data: {decryptedData}");

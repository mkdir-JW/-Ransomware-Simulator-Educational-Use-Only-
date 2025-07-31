# 🛡️ Ransomware Simulator (Educational Use Only)

**A safe, local simulation of a ransomware attack – for educational purposes only.**

---

## 📘 Description

This project is an **educational ransomware simulator** that demonstrates how a ransomware attack might encrypt files, display a ransom note, and (for learning purposes) recover the encrypted data using stored keys.

> ⚠️ **WARNING:** This tool is strictly for educational and demonstrative purposes. Never use it with malicious intent or on real user data. All operations are performed in a temporary test folder.

---

## 🔍 Features

- Generates a user-specified number of test `.txt` files
- Simulates a ransomware attack:
  - Encrypts all test files using **AES-256 encryption**
  - Creates a realistic-looking ransom note with fake instructions
- Simulates data recovery:
  - Extracts the encryption key and IV from the ransom note
  - Decrypts the files back to their original content
- Cleans up the test environment after the simulation is complete

---

## 🛠️ Technologies Used

- C#
- .NET Console Application
- System.IO for file operations
- System.Security.Cryptography (AES encryption)

---

## ▶️ How to Use

1. Clone the repository and open the solution in Visual Studio (or run via `dotnet run`).
2. Enter the number of test files you want to generate when prompted.
3. Observe the simulation steps:
   - Files are encrypted.
   - A ransom note is created.
   - Files are decrypted using stored keys.
   - Test folder is removed.

---

## 🧪 Sample Output

<img width="942" height="758" alt="image" src="https://github.com/user-attachments/assets/479076cc-2ccf-4500-9932-2377746118bd" />

---

## 📄 License

MIT License – use responsibly.

---

## 🙋 Author

Project by an enthusiast of cybersecurity and educational software.  
Feel free to contribute or suggest improvements.

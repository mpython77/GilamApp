# Gilam App 🧼

![Unity Badge](https://img.shields.io/badge/Engine-Unity-5C2D91?logo=unity&logoColor=white)
![Firebase Badge](https://img.shields.io/badge/Database-Firebase-FFCA28?logo=firebase&logoColor=white)
![C# Badge](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)
![Android Badge](https://img.shields.io/badge/Platform-Android-3DDC84?logo=android&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg)

## 📖 Description

**Gilam App** is a comprehensive mobile application designed specifically for carpet washing companies to streamline their business operations. The application provides a centralized system for managing customer orders, tracking order processing stages, and monitoring financial performance in real-time.

With an intuitive interface and powerful features, Gilam App helps carpet washing businesses improve efficiency, reduce errors, and provide better service to their customers.

## ✨ Key Features

### Order Management
- 📝 **Create New Orders** - Quickly add new customer orders with detailed information
- 🔍 **Search Orders** - Find orders easily using various search criteria
- ✏️ **Edit Orders** - Update order information and status as needed
- 🗑️ **Delete Orders** - Remove cancelled or completed orders

### Real-Time Tracking
- ⏱️ **Live Order Status** - Track each order's current processing stage in real-time
- 🔄 **Stage Updates** - Update order stages (Received → Washing → Drying → Ready → Delivered)
- 📊 **Active Orders Dashboard** - View all active orders at a glance
- 🔔 **Notifications** - Get alerts for important order updates

### Financial Management
- 💰 **Income Tracking** - Calculate total income from completed orders
- 💵 **Payment Status** - Monitor paid and unpaid orders
- 📈 **Revenue Analytics** - View daily, weekly, and monthly revenue reports
- 💳 **Payment Methods** - Track different payment methods (Cash, Card, Transfer)

### Statistics & Analytics
- 📊 **General Statistics** - Comprehensive overview of business performance
- 📉 **Order Trends** - Analyze order patterns and trends over time
- 👥 **Customer Insights** - Track customer order history and preferences
- 📅 **Time-Based Reports** - Generate reports for specific date ranges

### User Experience
- 🌐 **Fully Online** - Cloud-based system with real-time synchronization
- 📱 **Mobile Optimized** - Designed specifically for mobile devices
- 🎨 **Clean Interface** - User-friendly and intuitive design
- 🔐 **Secure Authentication** - Firebase-based secure login system

## 🛠️ Technologies Used

| Technology | Purpose | Version |
|------------|---------|---------|
| **Unity Engine** | Cross-platform development framework | 2021.3+ |
| **C#** | Primary programming language | 9.0+ |
| **Firebase Realtime Database** | Cloud-based real-time data storage | Latest |
| **Firebase Authentication** | Secure user authentication | Latest |
| **Firebase Storage** | Media and file storage | Latest |
| **Unity UI** | User interface development | Built-in |
| **DOTween** | Smooth animations and transitions | 1.2.0+ |

## 📋 System Requirements

### Development Requirements
- Unity Hub (Latest version)
- Unity Editor 2021.3 LTS or higher
- Visual Studio 2019/2022 or Visual Studio Code
- Firebase account and project
- Git for version control

### Device Requirements
- Android 7.0 (API Level 24) or higher
- Minimum 2GB RAM
- 100MB free storage space
- Active internet connection

## ▶️ Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/mpython77/GilamApp.git
cd GilamApp
```

### 2. Open in Unity
1. Open Unity Hub
2. Click "Add" and select the cloned project folder
3. Select Unity version 2021.3 LTS or higher
4. Click on the project to open it

### 3. Configure Firebase

#### Step 1: Create Firebase Project
1. Go to [Firebase Console](https://console.firebase.google.com/)
2. Click "Add Project"
3. Enter project name and follow the setup wizard

#### Step 2: Add Android App
1. In Firebase Console, click "Add App" → Android
2. Enter your package name: `com.yourcompany.gilamapp`
3. Download `google-services.json`

#### Step 3: Add Configuration File
1. Place `google-services.json` in `Assets/StreamingAssets/`
2. Ensure the file is included in the build

#### Step 4: Enable Firebase Services
In Firebase Console, enable:
- **Authentication** → Email/Password
- **Realtime Database** → Start in test mode (configure rules later)
- **Storage** → Start in test mode

### 4. Build the Application

#### For Android
1. Go to `File` → `Build Settings`
2. Select `Android` platform
3. Click `Switch Platform`
4. Click `Build` or `Build and Run`
5. Choose output location for APK

#### Build Settings Recommendations
- **Minimum API Level**: Android 7.0 (API 24)
- **Target API Level**: Latest stable
- **Scripting Backend**: IL2CPP
- **Target Architectures**: ARM64

## 🖼️ Screenshots

<div align="center">

### Main Dashboard
<img src="https://github.com/mpython77/GilamApp/blob/main/photo_3_2025-12-24_14-34-32.jpg?raw=true" width="250" alt="Main Dashboard"/>

*Main screen showing active orders overview and quick actions*

---

### Accepted Orders List
<img src="https://github.com/mpython77/GilamApp/blob/main/photo_1_2025-12-24_14-34-31.jpg?raw=true" width="250" alt="Accepted Orders"/>

*List of all accepted orders with status indicators*

---

### Add New Order
<img src="https://github.com/mpython77/GilamApp/blob/main/photo_2_2025-12-24_14-34-32.jpg?raw=true" width="250" alt="Add New Order"/>

*Order creation form with customer and carpet details*

---

### Search Functionality
<img src="https://github.com/mpython77/GilamApp/blob/main/photo_4_2025-12-24_14-34-32.jpg?raw=true" width="250" alt="Search Screen"/>

*Advanced search interface for finding specific orders*

---

### Statistics Dashboard
<img src="https://github.com/mpython77/GilamApp/blob/main/photo_5_2025-12-24_14-34-32.jpg?raw=true" width="250" alt="Statistics"/>

*Comprehensive statistics and analytics view*

</div>



## 🔧 Configuration

### Firebase Database Rules
```json
{
  "rules": {
    "orders": {
      ".read": "auth != null",
      ".write": "auth != null"
    },
    "users": {
      "$uid": {
        ".read": "$uid === auth.uid",
        ".write": "$uid === auth.uid"
      }
    }
  }
}
```

### Firebase Storage Rules
```
rules_version = '2';
service firebase.storage {
  match /b/{bucket}/o {
    match /{allPaths=**} {
      allow read, write: if request.auth != null;
    }
  }
}
```

## 🚀 Usage Guide

### Creating a New Order
1. Tap the "+" button on the main screen
2. Enter customer information (Name, Phone, Address)
3. Add carpet details (Type, Size, Quantity)
4. Set price and expected delivery date
5. Tap "Save" to create the order

### Updating Order Status
1. Find the order in the list
2. Tap on the order to view details
3. Select new status from the dropdown
4. System automatically updates timestamp
5. Customer notification sent (if enabled)

### Viewing Statistics
1. Navigate to Statistics screen
2. Select date range for analysis
3. View revenue charts and trends
4. Export reports (optional)

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow C# naming conventions
- Comment complex logic
- Write unit tests for new features
- Update documentation as needed

## 🐛 Known Issues & Roadmap

### Known Issues
- [ ] Occasional sync delay on slow connections
- [ ] Statistics may take time to load with large datasets

### Upcoming Features
- [ ] SMS notifications for customers
- [ ] Multi-language support
- [ ] Barcode/QR code scanning for orders
- [ ] Employee management system
- [ ] Customer loyalty program
- [ ] Automated backup system
- [ ] iOS version

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
```
MIT License

Copyright (c) 2025 mpython77

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## 📞 Contact & Support

- **Developer**: mpython77
- **GitHub**: [@mpython77](https://github.com/mpython77)
- **Email**: muhammadjon0513@gmail.com
- **Issues**: [GitHub Issues](https://github.com/mpython77/GilamApp/issues)

## 🙏 Acknowledgments

- Unity Technologies for the amazing game engine
- Firebase team for excellent backend services
- All contributors and testers
- Carpet washing businesses who provided valuable feedback

## 📊 Project Status

![GitHub last commit](https://img.shields.io/github/last-commit/mpython77/GilamApp)
![GitHub issues](https://img.shields.io/github/issues/mpython77/GilamApp)
![GitHub stars](https://img.shields.io/github/stars/mpython77/GilamApp)
![GitHub forks](https://img.shields.io/github/forks/mpython77/GilamApp)

---

<div align="center">

**⭐ If you find this project useful, please consider giving it a star! ⭐**

Made with ❤️ by [mpython77](https://github.com/mpython77)

</div>

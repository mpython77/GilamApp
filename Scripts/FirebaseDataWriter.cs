using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseDataWriter : MonoBehaviour
{
    private DatabaseReference dbReference;

    void Start()
    {
        // Firebase dependencies tekshirish
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;

                // Ma'lumot yozamiz
                WriteUserData("User1", "Ali", "ali@gmail.com");
            }
            else
            {
                Debug.LogError("Firebase mavjud emas: " + task.Result.ToString());
            }
        });
    }

    void WriteUserData(string userId, string name, string email)
    {
        // Obyekt sifatida yoziladigan dictionary
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        // Path: "Users/User1"
        dbReference.Child("Users").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Ma'lumot yozildi: " + userId);
            }
            else
            {
                Debug.LogError("Xatolik yuz berdi: " + task.Exception);
            }
        });
    }
}

// User ma'lumotlarini saqlash uchun class
[System.Serializable]
public class User
{
    public string username;
    public string email;

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}

using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseExample : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Start()
    {
        // Firebase-ni ishga tushirish
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                // Firebase Database-ga ulanish
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                // "salom" so‘zini bazaga yozish
                SaveDataToFirebase();
                
                // Bazadan ma'lumotni o‘qib chop etish
                ReadDataFromFirebase();
            }
            else
            {
                Debug.LogError("Firebase dependencies could not be resolved: " + task.Exception);
            }
        });
    }

    void SaveDataToFirebase()
    {
        // "message" tuguniga "salom" so‘zini yozish
        databaseReference.Child("message").SetValueAsync("salom").ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Successfully saved 'salom' to Firebase Realtime Database!");
            }
            else
            {
                Debug.LogError("Failed to save data: " + task.Exception);
            }
        });
    }

    void ReadDataFromFirebase()
    {
        // "message" tugunidan ma'lumotni o‘qish
        databaseReference.Child("message").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string data = snapshot.Value?.ToString();
                if (data != null)
                {
                    Debug.Log("Data from Firebase: " + data);
                }
                else
                {
                    Debug.Log("No data found in Firebase at 'message' node.");
                }
            }
            else
            {
                Debug.LogError("Failed to read data: " + task.Exception);
            }
        });
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    private FirebaseApp _app;
    public float getX;
    public float getZ;

    void Start()
    {
        //Comprueba las dependencias de forma asincrona de la base de datos
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            //Si las dependencias son correctas, deja acceder a la base de datos
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                //Instancia la base de datos al programa
                _app = Firebase.FirebaseApp.DefaultInstance;
            //Si las dependencias son correctas, muestra un mensaje de error
            } else {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    /*private void AddData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("apple").Document("position");
        Dictionary<string, float> data = new Dictionary<string, float>
        {
            { "X", getX },
            { "Z", getZ },
        };
        docRef.SetAsync(data).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the position document in the apple collection.");
        });
    }*/

    /*private void GetData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(String.Format("User: {0}", document.Id));
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(String.Format("First: {0}", documentDictionary["First"]));
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(String.Format("Middle: {0}", documentDictionary["Middle"]));
                }
                Debug.Log(String.Format("Last: {0}", documentDictionary["Last"]));
                Debug.Log(String.Format("Born: {0}", documentDictionary["Born"]));
            }
            Debug.Log("Read all data from the users collection.");
        });
    }*/

    /**
    *   Metodo para registrar o acceder a la base de datos   
    */
    private void Login()
    {
        //Instanciamos la autenticacion de la base de datos
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //Si el usuario ya esta registrado, deja acceder por defecto
        if (auth.CurrentUser != null)
        {
            Debug.Log("Usuario ya autentificado");
            //AddData();
            return;
        }
        //Autentificamos al usuario de forma anonima
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            //Si la tarea es cancelada, mostramos un mensaje de error
            if (task.IsCanceled) {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            //Si la tarea es cancelada, mostramos un mensaje de error
            if (task.IsFaulted) {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }
            //Si no hay ningun fallo, se autentifica el usuario y lo mostramos en la consola
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    /*private void Update()
    {
        getX = GameObject.FindWithTag("Object").transform.position.x;
        getZ = GameObject.FindWithTag("Object").transform.position.z;
    }*/

    /*private void UpdateFirestore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("apple").Document("position");
        Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "X", getX },
            { "Z", getZ },
        };
        docRef.UpdateAsync(updateData).ContinueWithOnMainThread(task => {
            Debug.Log("Update data to the position document in the apple collection.");
        });
    }*/
}

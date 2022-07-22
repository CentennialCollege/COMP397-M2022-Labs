using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
class PlayerData
{
    public string position;
    public string rotation;
    // abilities
    public string strength;
    public string health;

    public override string ToString()
    {
        return $"Abilities: \nStrength: {strength}\nHealth: {health}\nTransform Info:\nPosition: {position}\nRotation: {rotation}\n";
    }
}



[System.Serializable]
public class GameSaveManager
{
    /************** SINGLETON PATTERN ****************/
   // Step 1. -- Declare a private static instance of the class
   private static GameSaveManager m_instance = null;

   // Step 2. -- Declare your constructor and Destructor functions private
   private GameSaveManager(){}
   
   // Step 3. -- Create an Instance Method / Property (used as the "gateway" to the class itself)
   public static GameSaveManager Instance()
   {
       return m_instance ??= new GameSaveManager();
   }
   /**************************************************/

   // Serialize Data (Encodes the data)
   public void SaveGame(Transform playerTransform)
   {
        // Step 1. - Create a Binary Formatter object and setup a datapath for the file
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        
        // Step 2 - Create a PlayerData object
        var data = new PlayerData
        {
            // Step 3 - Save the data to the PlayerData object
            position = JsonUtility.ToJson(playerTransform.position),
            rotation = JsonUtility.ToJson(playerTransform.rotation.eulerAngles),
            health = "80",
            strength = "27"
        };

        // Step 4 - Serialize the data and close the file
        bf.Serialize(file, data);
        file.Close();
      
       Debug.Log("Game Data Saved!");
   }

   // Deserializes Data (Decodes the data)
   public void LoadGame(Transform playerTransform)
   {
       if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
       {
            // Step 1. - Create a Binary Formatter object and setup a datapath for the file
            var bf = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);

            // Step 2 - Create a PlayerData object and deserialize the data from the file
            var data = (PlayerData)bf.Deserialize(file);
            Debug.Log(data); // Show the player data

            file.Close();

            // Step 3 - Adjust the Player Transform accordingly (i.e., bring the data back into the game)
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;
            playerTransform.position = JsonUtility.FromJson<Vector3>(data.position);
            playerTransform.rotation = Quaternion.Euler(JsonUtility.FromJson<Vector3>(data.rotation));
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = true;
            Debug.Log("Game Data Loaded!");
       }
       else
       {
           Debug.Log("No Game Data found!");
       }
   }

   public void ResetData()
   {
       if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
       {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Game Data Cleared!");
       }
       else
       {
           Debug.Log("No Save Data to Delete!");
       }
   }
   
}

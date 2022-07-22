using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       PlayerPrefs.SetString("playerPosition", JsonUtility.ToJson(playerTransform.position));
       PlayerPrefs.SetString("playerRotation", JsonUtility.ToJson(playerTransform.rotation.eulerAngles));
       PlayerPrefs.Save();
       Debug.Log("Game Data Saved!");
   }

   // Deserializes Data (Decodes the data)
   public void LoadGame(Transform playerTransform)
   {
       if (PlayerPrefs.HasKey("playerPosition") && PlayerPrefs.HasKey("playerRotation"))
       {
           playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;
           playerTransform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("playerPosition"));
           playerTransform.rotation = Quaternion.Euler(JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("playerRotation")));
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
       PlayerPrefs.DeleteAll();
       Debug.Log("Game Data Cleared!");
    }
   
}

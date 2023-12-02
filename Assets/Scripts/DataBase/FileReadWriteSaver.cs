using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class FileReadWriteDataSaver : DataSaveBase
{
    private List<PlayerProfile> playerProfiles = new List<PlayerProfile>();

    private PlayerProfile player = new PlayerProfile();

    private PlayerProfile player1 = new PlayerProfile();
    private PlayerProfile player2 = new PlayerProfile();


    public TextMeshProUGUI playername;
    public int carType = 0;
    private void Awake()
    {
      
    }

    public void CreatProfile()
    {
       
      player.playerName = playername.text;
      player.carType = carType;
      playerProfiles.Add(player);
    }
    public override void Save()
    {
        CreatProfile();
        string path = Application.persistentDataPath + "/PlayerProfiles.txt";

        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (var playerProfile in playerProfiles)
            {
                // Write each player profile as a line in CSV format
                writer.WriteLine($"{playerProfile.playerName},{player.carType}");
            }
        }
    }

    public override void Load()
    {
        string path = Application.persistentDataPath + "/PlayerProfiles.txt";

        if (File.Exists(path))
        {
            playerProfiles.Clear();

            using (StreamReader reader = new StreamReader(path))
            {
                List<string> lines = new List<string>();

                // Read all lines into a list
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                // Determine the starting index based on the desired number of profiles
                int startIndex = Mathf.Max(0, lines.Count - 2); // Retrieve the last 2 profiles

                for (int i = startIndex; i < lines.Count; i++)
                {
                    Debug.Log("line number " + i);
                    string[] values = lines[i].Split(',');

                    // Ensure that the line has the expected number of values
                    if (i == 1)
                    {
                        player1.playerName = values[0];
                        player1.carType = int.Parse(values[1]);

                        playerProfiles.Add(player1);
                    }else
                        if(i == 2)
                    {
                        player2.playerName = values[0];
                        player2.carType = int.Parse(values[1]);

                        playerProfiles.Add(player2);
                    }

                    
                    
                }
            }

            foreach (var playerProfile in playerProfiles)
            {
               Debug.Log("Player Name: " + playerProfile.playerName + playerProfile.carType);
            }
        }

    }
}
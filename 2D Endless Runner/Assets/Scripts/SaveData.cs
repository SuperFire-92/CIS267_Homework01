using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    private static float[] scores;
    private const string fileName = "/highscore.sc";

    //Saves a new score to the file
    public static void saveNewScore(float s)
    {
        loadScores();
        int scorePlacement = -999;
        //Check to see if a high score was made
        for (int i = 0; i < 5; i++)
        {
            if (s > scores[i])
            {
                scorePlacement = i;
                i = 999;
            }
        }
        //If one was made
        if (scorePlacement != -999)
        {
            //Put it into it's new place
            for (int i = 4; i > scorePlacement; i--)
            {
                scores[i] = scores[i - 1];
            }
            scores[scorePlacement] = s;

            //Save it
            string path = Application.persistentDataPath + fileName;

            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, scores[0] + "," + scores[1] + "," + scores[2] + "," + scores[3] + "," + scores[4]);

            stream.Close();
        }
    }

    //Load all highscores
    public static float[] loadScores()
    {
        string path = Application.persistentDataPath + fileName;

        //If a highscore has been made before
        if (File.Exists(path))
        {
            //Load all of the highscores
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            string listOfScores = (string) bf.Deserialize(stream);

            stream.Close();

            scores = parseScores(listOfScores);

            //Display them to the console
            Debug.Log("Highscores: " + scores[0] + " " + scores[1] + " " + scores[2] + " " + scores[3] + " " + scores[4]);

            return scores;
        }
        //If none have been made
        else
        {
            //Return dummy values
            Debug.LogError("File Not Found In " + path);
            scores = new float[] { -999, -999, -999, -999, -999 };
            return scores;
        }
    }

    //Parse through the scores to make a float[]
    public static float[] parseScores(string s)
    {
        float[] loadedScores = new float[5];
        string[] splitScores = s.Split(',');

        for (int i = 0; i < 5; i++)
        {
            loadedScores[i] = float.Parse(splitScores[i]);
        }

        return loadedScores;
    }
}

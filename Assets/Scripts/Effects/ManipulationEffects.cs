using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public static class ManipulationEffects
{
    public static IEnumerator MessWithBackground(GameController controller)
    {
        List<Color> colors = new List<Color>() {new Color(128, 0, 128), Color.red, Color.white, Color.red, Color.black};
        float timeRemaining = 0.5f;
        float interval = 0.05f;

        while (timeRemaining > 0)
        {
            controller.background.color = colors[(int) (timeRemaining * 10) % 5];
            timeRemaining -= interval;
            yield return new WaitForSeconds(interval);
        }
		
        controller.background.color = Color.black;
    }

    public static string RandomDistortedString(GameController controller)
    {
        string distortionCharacters =
            "̸̴̨̡̧̧̧̧̛̛͉͔̹͈̞͈̪͔̯̫̲̮͍̗͙͕̰̝̗͙̼͕͇͍̦̥̻̞͍̜̦̬͙̯̭̟̫̣͇̙̳̠̠̮̝̩͍͇̻̥̹̜̹̗̳͙͚̦̮͇͉̭͍͚͎̺͍̦̮͕̯̹͖̘̺̱̣͓̝͔̦͎͉̮̠̣̟̥̟̠̳̼̗̳͇͈̬͕̙̰͉̪̣̣̲͎̰̄̀̓̔͂͗̈́͐̍̓̈̈̇̓͐͗̅́̔́̀͋̐̒̋͛̽̈́̐̀͑̄̀̉̑̉̋͌̑̓̈́̍̈́̉͗̉̉̓̀̽͗̿̓̓̌̃͌̃̓͊̈̂̇͛̂̿͐͊̑͂̌͗̓̏̅̓̽͌͋̂̅̄̏͌̑̊͐́̓̒̈̐̾͒̑̋̀̈́̊̀͋̔̉̿͑̀̇͐͛̒̎̓̓̍͐̓̐̌̈́̀̋̃̆̌͐̽̐̄̿̉͊̋̓́͛̐͋̑̾̾̈̅́̽̾͒̐͑̂͑͂̾͌̌͗͌̑́̑̓̒͋͆̅͐̔̕̕̕͘͘̚̕̕͜͜͜͜͠͝͝͝͝͝͝͝͠͠͠͠͝ͅͅͅͅa̴̸̸̡̡̧̢̢̢̨̧̢̛̛̲̯̮̠̖̰͎̱̜̬͚͍̤͉̯͎͓̺̺͉̘̱͎̖̰̟͎̟͈̮̤͔̠̙͍̗͉̬͖̠͈̟̖̣̣̫̯̭͔̝̻̼͍̟̪̭̦̜̹̙͔͓̪̯̹̤̲̘͎̱̖͇̟̬̲̩̞͚̓͑̓̈͑̈̋̒͌̈́̀̅̿̓͒͋̾̽̑̏̌̅̓͂̊͂̽́̓̈́̀͒̾̊͌̒͆̊̿̌̀͛͌̊̏̿̈́͋̋͂̾́͊͒̓͛̌̌͘͘͘͜͜͜͝ͅͅ";
        int stringLength = Random.Range(8, 58);
        Dictionary<string, List<string>> dOne = controller.LoadDictionaryFromCsvFile("characterInteractionDescriptions");
        Dictionary<string, string> dTwo = controller.LoadDictionaryFromFile("homeCaveDescriptions");
        List<string> randomStrings = dTwo.Values.ToList();
        randomStrings.AddRange(dOne.Values.SelectMany(x => x).ToList());
        
        string s = "";
        for (int i = 0; i < stringLength; i++)
        {
            string randomWordFromRandomList = "";
            int rng = Random.Range(0, 100);
            if (rng < randomStrings.Count)
            {
                List<string> temp = new List<string>(randomStrings[rng].Split(' '));
              for (int j = 0; j < temp.Count; j++)
              {
                  if (Random.Range(0, 100) < 20)
                  {
                      temp[j] = distortionCharacters.Substring(distortionCharacters.Length - Random.Range(1, 40)) +
                                temp[j] + distortionCharacters.Substring(Random.Range(1, 40));
                  }
              }

              randomWordFromRandomList = string.Join(" ", temp);
              
            }

            s = s + randomWordFromRandomList;
        }
        
        return s;
    }
    
    public static string RandomDistortedString()
    {
        string distortionCharacters =
            "̸̴̨̡̧̧̧̧̛̛͉͔̹͈̞͈̪͔̯̫̲̮͍̗͙͕̰̝̗͙̼͕͇͍̦̥̻̞͍̜̦̬͙̯̭̟̫̣͇̙̳̠̠̮̝̩͍͇̻̥̹̜̹̗̳͙͚̦̮͇͉̭͍͚͎̺͍̦̮͕̯̹͖̘̺̱̣͓̝͔̦͎͉̮̠̣̟̥̟̠̳̼̗̳͇͈̬͕̙̰͉̪̣̣̲͎̰̄̀̓̔͂͗̈́͐̍̓̈̈̇̓͐͗̅́̔́̀͋̐̒̋͛̽̈́̐̀͑̄̀̉̑̉̋͌̑̓̈́̍̈́̉͗̉̉̓̀̽͗̿̓̓̌̃͌̃̓͊̈̂̇͛̂̿͐͊̑͂̌͗̓̏̅̓̽͌͋̂̅̄̏͌̑̊͐́̓̒̈̐̾͒̑̋̀̈́̊̀͋̔̉̿͑̀̇͐͛̒̎̓̓̍͐̓̐̌̈́̀̋̃̆̌͐̽̐̄̿̉͊̋̓́͛̐͋̑̾̾̈̅́̽̾͒̐͑̂͑͂̾͌̌͗͌̑́̑̓̒͋͆̅͐̔̕̕̕͘͘̚̕̕͜͜͜͜͠͝͝͝͝͝͝͝͠͠͠͠͝ͅͅͅͅa̴̸̸̡̡̧̢̢̢̨̧̢̛̛̲̯̮̠̖̰͎̱̜̬͚͍̤͉̯͎͓̺̺͉̘̱͎̖̰̟͎̟͈̮̤͔̠̙͍̗͉̬͖̠͈̟̖̣̣̫̯̭͔̝̻̼͍̟̪̭̦̜̹̙͔͓̪̯̹̤̲̘͎̱̖͇̟̬̲̩̞͚̓͑̓̈͑̈̋̒͌̈́̀̅̿̓͒͋̾̽̑̏̌̅̓͂̊͂̽́̓̈́̀͒̾̊͌̒͆̊̿̌̀͛͌̊̏̿̈́͋̋͂̾́͊͒̓͛̌̌͘͘͘͜͜͜͝ͅͅ";
        int stringLength = Random.Range(0, 15);

        var random = new System.Random();
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        string undistorted = new string(Enumerable.Repeat(chars, stringLength)
            .Select(s => s[random.Next(s.Length)]).ToArray());

 /*       string s = "";
        for (int i = 0; i < stringLength; i++)
        {
            string randomWordFromRandomList = "";
            int rng = Random.Range(0, 100);
            if (rng < randomStrings.Count)
            {
                List<string> temp = new List<string>(randomStrings[rng].Split(' '));
                for (int j = 0; j < temp.Count; j++)
                {
                    if (Random.Range(0, 100) < 20)
                    {
                        temp[j] = distortionCharacters.Substring(distortionCharacters.Length - Random.Range(1, 40)) +
                                  temp[j] + distortionCharacters.Substring(Random.Range(1, 40));
                    }
                }

                randomWordFromRandomList = string.Join(" ", temp);
              
            }

            s = s + randomWordFromRandomList;
        }
        
        return s; */

        return undistorted;
    }
}

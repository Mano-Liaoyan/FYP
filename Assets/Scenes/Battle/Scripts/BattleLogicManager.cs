using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogicManager : MonoBehaviour
{
    public static Vector3 EndPoint;
    public float speed = 15.0f;

    // Start is called before the first frame update  
    void Start()
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[2];
        EndPoint.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<BattleDataManager.MoveSwitch.Count; i++)
        {
            if (BattleDataManager.MoveSwitch[i])
            {
                moveCharacter(i);
                print("start move character");
                //GameObject currObj = BattleDataManager.FriendlyCharacters[i];
                //currObj.transform.position = Vector3.MoveTowards(currObj.transform.position, EndPoint, speed * Time.deltaTime);
                
            }

        }
    }
    public void moveCharacter(int i)
    {
        print("start moving");
        GameObject currObj = BattleDataManager.FriendlyCharacters[i];
        Vector3 tmp = currObj.transform.localPosition;
        tmp.z = 0;
        currObj.transform.localPosition = tmp;
        currObj.transform.localPosition = Vector3.MoveTowards(currObj.transform.localPosition, EndPoint, speed * Time.deltaTime);
        if (Vector3.Distance(currObj.transform.position, EndPoint) < 500f)
        {
            BattleDataManager.MoveSwitch[i] = false;
            print("End moving");
        }

    }

    public static void updateMovement(string character, int randomIdx)
    {
        EndPoint = BattleDataManager.EnemyCharactersPostions[randomIdx];
        EndPoint.z = 0;
        for (int i=0; i<BattleDataManager.FriendlyCharacters.Count; i++)
        {
            if(BattleDataManager.FriendlyCharacters[i].name.Contains(character))
            {
                BattleDataManager.MoveSwitch[i] = true;

            }
        }

    }

}

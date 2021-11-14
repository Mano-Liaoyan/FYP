using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;
using UnityEngine;

public class LBManager : MonoBehaviour
{
    GameObject ScrollRect;
    GameObject Content;
    List<GameObject> List_me;

    // Start is called before the first frame update
    void Start()
    {
        List_me = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void getLeaderboard()
    {
        await getLeaderboardAsync();
    }

    public async Task getLeaderboardAsync()
    {
        // 需要更改default选项
        // 还要加username
        const string leaderboardId = "score";
        //print("start");
        ScrollRect = GameObject.Find("ScrollRect");
        Content = GameObject.Find("Content");
        print(Content);
        foreach (Transform child in Content.transform)
        {
            List_me.Add(child.gameObject);
        }

        var my_rank = await User.client.ListLeaderboardRecordsAroundOwnerAsync(User.session, leaderboardId, User.session.UserId, null);

        foreach (var r in my_rank.Records)
        {
            ScrollRect.transform.Find("List_Me/Text_999").GetComponent<TMP_Text>().text = r.Rank;
            ScrollRect.transform.Find("List_Me/Text_Score").GetComponent<TMP_Text>().text = r.Score;
            ScrollRect.transform.Find("List_Me/Text_NickName").GetComponent<TMP_Text>().text = r.Username;
            Debug.LogFormat("My score: Record for '{0}' score '{1}'", r.Username, r.Score);
        }
        int counter = 0;

        var result = await User.client.ListLeaderboardRecordsAsync(User.session, leaderboardId,null,null,9);


        foreach (var r in result.Records)
        {

            print(List_me[counter]);
            List_me[counter].transform.Find("Text_Score").GetComponent<TMP_Text>().text = r.Score;
            List_me[counter].transform.Find("Text_NickName").GetComponent<TMP_Text>().text = r.Username;
            print(List_me[counter].transform.Find("Text_Score"));
            Debug.LogFormat("Record for '{0}' score '{1}'", r.Username, r.Score);
            counter++;
        }
        //if (result.NextCursor != null)
        //{
        //    result = await User.client.ListLeaderboardRecordsAsync(User.session, leaderboardId, null, null, 10, result.NextCursor);
        //    //User.client.ListLeaderboardRecordsAsync()
        //    foreach (var r in result.Records)
        //    {
        //        Debug.LogFormat("Record for '{0}' score '{1}'", r.Username, r.Score);
        //        List_me[counter].transform.Find("Text_Score").GetComponent<TMP_Text>().text = r.Score;
        //        print(List_me[counter].transform.Find("Text_Score"));
        //        counter++;
        //    }
        //}

    }
}

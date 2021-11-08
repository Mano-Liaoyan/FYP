using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LBManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void getLeaderboard() {
        print("??");
        _ = getLeaderboardAsync();
    }

    public async Task getLeaderboardAsync() {
        const string leaderboardId = "score";
        print("Enter leaderboard;");
        var my_rank = await User.client.ListLeaderboardRecordsAroundOwnerAsync(User.session, leaderboardId, User.session.UserId, null);
        foreach (var r in my_rank.Records) {
            Debug.LogFormat("My score: Record for '{0}' score '{1}'", r.Username, r.Score);
        }

        var result = await User.client.ListLeaderboardRecordsAsync(User.session, leaderboardId);
        foreach (var r in result.Records) {
            Debug.LogFormat("Record for '{0}' score '{1}'", r.Username, r.Score);
        }
        print(result.NextCursor);
        if (result.NextCursor != null) {
            result = await User.client.ListLeaderboardRecordsAsync(User.session, leaderboardId, null, null,10,result.NextCursor);
            //User.client.ListLeaderboardRecordsAsync()
            foreach (var r in result.Records) {
                Debug.LogFormat("Record for '{0}' score '{1}'", r.Username, r.Score);
            }
        }
    }
}

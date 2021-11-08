using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using Nakama;
using System.Threading.Tasks;
using Nakama.TinyJson;
using System;

public class GainOrLosePoint : MonoBehaviour
{
    private List<string> buildings = new List<string>();
    
    private List<GameObject> areas = new List<GameObject>();
    private AbstractLocationProvider _locationProvider = null;
    private ISession _curr_session = null;
    private float lastTime;
    private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
        _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
        _curr_session = User.session;
        if(_curr_session == null)
        {
            Debug.LogError("Cannot get session.");
        }
    }

    // Update is called once per frame
    void Update()
    {
//        Location currLocation = _locationProvider.CurrentLocation;
//        if (currLocation.IsLocationServiceInitializing)
//        {
//            print("location services are initializing");
//        }
//        else
//        {
//#if UNITY_EDITOR
//            if (!currLocation.IsLocationServiceEnabled)
//            {
//                print("location services not enabled");
//            }
//            else
//#endif
//            {
//                if (currLocation.LatitudeLongitude.Equals(Vector2d.zero))
//                {
//                    print("Waiting for location ....");
//                }
//                else
//                {
//                    examineLocation(currLocation.LatitudeLongitude);
//                    //print(string.Format("{0}", currLocation.LatitudeLongitude));
//                }
//            }
//        }
    }

    public static async void examineLocation(string buildingName)
    {
        if (buildingName.Contains("uic"))
        {
            minusScore();
        } else if (buildingName.Contains("gym") || buildingName.Contains("t"))
        {
            addScore();
        }
        await examineUpdateScoreAsync();
    }

    public static async Task examineUpdateScoreAsync() {
        if (Time.realtimeSinceStartup - User.lastTime > 5) {
            print("Time: " + Time.realtimeSinceStartup);
            print("Score: " + User.score);
            User.lastTime = Time.realtimeSinceStartup;
        
            try {
                Dictionary<string, string> payload = new Dictionary<string, string>();
                payload.Add("score", Convert.ToString(User.score));
                payload.Add("user_id", User.session.UserId);
                var response = await User.client.RpcAsync(User.session, "updateUserScore", payload.ToJson());
                print(response);
                updateLeaderboardAsync();
            } catch (ApiResponseException ex) {
                Debug.LogFormat("Error: {0}", ex.Message);
            }
        }
    }

    public static async Task updateLeaderboardAsync() {
        const string leaderboardID = "score";
        var r = await User.client.WriteLeaderboardRecordAsync(User.session, leaderboardID, User.score);
        print(r);
    }

    public static void addScore()
    {
        User.score++;
    }

    public static void minusScore()
    {
        User.score--;
    }
    
}

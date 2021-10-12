using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using Nakama;

public class GainOrLosePoint : MonoBehaviour
{
    private List<string> buildings = new List<string>();
    
    private List<GameObject> areas = new List<GameObject>();
    private AbstractLocationProvider _locationProvider = null;
    private ISession _curr_session = null;
    // Start is called before the first frame update
    void Start()
    {
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

    public static void examineLocation(string buildingName)
    {
        if (buildingName.Contains("uic"))
        {
            minusScore();
        } else if (buildingName.Contains("gym") || buildingName.Contains("t"))
        {
            addScore();
        }
        print(User.score);
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

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UserCollection: MonoBehaviour {
    public Dictionary<string, User> allUsers = new Dictionary<string, User>();

    public GameObject userVisualPrefab;


     public void HandleTick(Tick tick) {
        //Debug.Log("Got tick " + tick);

        User user = userForUUID(tick.uuid);
        user.Upness = tick.upness;
        user.Velocity = tick.velocity;
     }

     public void HandleName(Name name) {
        //Debug.Log("Got tick " + tick);

        User user = userForUUID(name.uuid);
        user.Name = name.name;
     }

     public void HandleNamedColor(NamedColor namedColor) {
        //Debug.Log("Got tick " + tick);

        User user = userForUUID(namedColor.uuid);
        user.ColorName = namedColor.colorName;
     }



     private User userForUUID(string uuid) {
         User user = null;
         if (allUsers.TryGetValue(uuid, out user)) {
            //Debug.Log("found user " + user);
         } else {
            //Debug.Log("found no user.");
         }
         if (user == null) {
             user = new User();
             user.uuid = uuid;
             allUsers[uuid] = user;
             Vector3 position = randomLocation();
             user.userVisual = Instantiate(userVisualPrefab, position, new Quaternion()).GetComponent<UserVisual>();
             user.userVisual.Name = uuid;
             user.userVisual.BasePosition = position;
             Debug.Log("Created new user " + user.uuid); 
         }
         else {
             //Debug.Log("Using existing user");
         }
        return user;
     }

     public Vector3 randomLocation() {
         float x = UnityEngine.Random.Range(-5f, 5f);
         float y = UnityEngine.Random.Range(-3f, 3f);
         return new Vector3(x, y, 0);
     }
}

 public class User {
     public string uuid;
    private string name;
    private string colorName;
    private float upness;
    public UserVisual userVisual;

    private DeviceVelocity velocity;
    public DeviceVelocity Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
            userVisual.Velocity = velocity;
        }
    }

    public float Upness
    {
        get
        {
            return upness;
        }

        set
        {
            upness = value;
            userVisual.Upness = upness;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
            userVisual.name = name;
        }
    }

    public string ColorName
    {
        get
        {
            return colorName;
        }

        set
        {
            colorName = value;
            userVisual.ColorName = colorName;
        }
    }

}
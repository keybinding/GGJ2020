using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanormalManager : MonoBehaviour
{
    public List<string> tags = new List<string>();
    public List<GameObject> places = new List<GameObject>();

    private HashSet<RunningObjectBehaviour> objects = new HashSet<RunningObjectBehaviour>();
    public HashSet<AvailablePlaceBehaviour> availablePlaces = new HashSet<AvailablePlaceBehaviour>();
    public Dictionary<string, HashSet<AvailablePlaceBehaviour>> ObjectAreasMap = new Dictionary<string, HashSet<AvailablePlaceBehaviour>>();
    public HashSet<AvailablePlaceBehaviour> allPlaces = new HashSet<AvailablePlaceBehaviour>();

    float curTime = 0.0f;
    public float resetInterval = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < places.Count; ++i)
        {
            if (!ObjectAreasMap.ContainsKey(tags[i % tags.Count])) ObjectAreasMap.Add(tags[i % tags.Count], new HashSet<AvailablePlaceBehaviour>());
            ObjectAreasMap[tags[i % tags.Count]].Add(places[i].GetComponent<AvailablePlaceBehaviour>());
        }
        foreach (var p in ObjectAreasMap) allPlaces.UnionWith(p.Value);
    }

    void Update()
    {
        if (curTime > resetInterval)
        {
            curTime = 0;
            ReshufleTask();
        }
        curTime += Time.deltaTime;
    }

    void ReshufleTask()
    {
        var objectsList = HashSetToList<RunningObjectBehaviour>(objects);

        //for (var i = 0; i < 3 && i < objectsList.Count; i++)
        {
            var index = Random.Range(0, objectsList.Count);
            var item = objectsList[index];
            //objectsList.RemoveAt(index);
            var places = new HashSet<AvailablePlaceBehaviour>();
            places.UnionWith(ObjectAreasMap[item.gameObject.tag]);
            places.IntersectWith(availablePlaces);
            var availablePlacesList = HashSetToList<AvailablePlaceBehaviour>(places);
            index = Random.Range(0, availablePlacesList.Count);
            var place = availablePlacesList[index];
            if (place.Object != null)
            {
                var tmp = place.Object;
                foreach (var p in allPlaces)
                {
                    if (p.Object == item)
                    {
                        p.Object = tmp;
                        break;
                    }
                }
                place.Object = item;
            }
            else
            {
                foreach (var p in allPlaces)
                {
                    if (p.Object == item)
                    {
                        p.Object = null;
                        break;
                    }
                }
                place.Object = item;
            }
        }
    }

    private List<T> HashSetToList<T>(HashSet<T> hashSet)
    {
        var result = new List<T>();
        foreach (var element in hashSet)
        {
            result.Add(element);
        }
        return result;
    }

    

    public void ObjectOutOfView(RunningObjectBehaviour obj)
    {
        if (obj.gameObject.tag == "place")
        {
            availablePlaces.Add((AvailablePlaceBehaviour)obj);
        }
        else
        {
            objects.Add(obj);
        }
    }

    public void ObjectInView(RunningObjectBehaviour obj)
    {
        if (obj.gameObject.tag == "place")
        {
            availablePlaces.Remove((AvailablePlaceBehaviour)obj);
        }
        else
        {
            objects.Remove(obj);
        }
    }
}

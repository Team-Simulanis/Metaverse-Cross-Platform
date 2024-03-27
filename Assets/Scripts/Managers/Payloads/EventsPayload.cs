using System;
using System.Collections.Generic;

[Serializable]
public class Event
{
    public string uuid;
    public string name;
    public string description;
    public DateTime startTime;
    public DateTime endTime;
    public string environmentId;
    public string type;
    public string status;
    public Organizer organizer;
}
[Serializable]
public class Organizer
{
    public string avatar;
    public string name;
    public string email;
    public string uuid;
}
[Serializable]
public class EventsListPayload
{
    public bool status;
    public List<Event> data;
}
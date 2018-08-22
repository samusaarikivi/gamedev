﻿using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ServiceManager : SingletonBehaviour<ServiceManager> {

    public Texture2D profilePicture;
    string profileName;
    public string profileId;

    public string ProfileName
    {
        get
        {
            if (profileName != "")
                return profileName;
            else
                return "new player";
        }
        private set
        {
            profileName = value;
        }
    }

    void Start () {
        ReportVanillaHighScores();
        DontDestroyOnLoad(gameObject);
        Authenticate();
    }
    void ReportVanillaHighScores()
    {
        if(!SaveLoad._vanillaFastestTimeReported)
        {
            SaveLoad._vanillaFastestTimeReported = true;
            ReportSprintTime(SaveLoad._fastestTime);
        }
    }
    void Init()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
    }
    public bool Authenticate() {
        Init();
        Social.Active.Authenticate(Social.localUser, (bool success) => {
            if (success)
            {
                ProfileName = Social.Active.localUser.userName; // UserName
                profileId = Social.Active.localUser.id; // UserID
                profilePicture = Social.Active.localUser.image; // ProfilePic
            }
        });
        if(Social.Active.localUser.authenticated)
        {
            return true;
        } else
        {
            return false;
        }
    }
    /*
     *  Public methods
     *  distance: CgkI_LSL0PQcEAIQAQ
     *  sprint: CgkI_LSL0PQcEAIQAg
     */
    public void ReportTestScore()
    {
        ReportDistance(7);
        ReportSprintTime(111);
    }
    public void ReportSprintTime(float time) {
        if (Social.Active.localUser.authenticated)
        {
            long ltime = (long)(time * 1000);
            Social.Active.ReportScore(ltime, "CgkI_LSL0PQcEAIQAg", (bool success) =>
            {

            });
        }

    }
    public void ReportDistance(int distance)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.Active.ReportScore(distance, "CgkI_LSL0PQcEAIQAQ", (bool success) =>
            {

            });
        }
    }
    public void ShowLeaderboards()
    {
        if (Authenticate())
        {
            Social.Active.ShowLeaderboardUI();
        }
    }
    public void ShowSprintLeaderboard()
    {
        if (Authenticate())
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI_LSL0PQcEAIQAg");
        }
    }
    public void ShowDistanceLeaderboard()
    {
        if (Authenticate())
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI_LSL0PQcEAIQAQ",(GooglePlayGames.BasicApi.UIStatus status) => { });
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace Ex01_Facebook.Logic
{
    public class Engine
    {
        public FacebookDatingFeature DatingFeature { get; set; }

        public FacebookGuessMyNameFeature GuessMyNameFeature { get; set; }

        public User LoggedInUser { get; set; }

        public LoginResult LastLoginResult { get; set; }

        private readonly string[] r_Permissions =
            {
            "public_profile",
           "email",
           "publish_to_groups",
           "user_birthday",
           "user_age_range",
           "user_gender",
           "user_link",
           "user_tagged_places",
           "user_videos",
           "publish_to_groups",
           "groups_access_member_info",
           "user_friends",
           "user_events",
           "user_likes",
           "user_location",
           "user_photos",
           "user_posts",
           "user_hometown"
        };

        public LoginResult LoginToFacebook()
        {
            LoginResult result;
            try
            {
                result = FacebookService.Login("337720197182315", r_Permissions);
            }
            catch (Exception)
            {
                // error has occurred while try to login to Facebook server.
                result = null;
            }

            if (result != null && !string.IsNullOrEmpty(result.AccessToken))
            {
                prepareFacebookApplication(result);
            }

            return result;
        }

        private void prepareFacebookApplication(LoginResult i_Result)
        {
            LastLoginResult = i_Result;
            LoggedInUser = i_Result.LoggedInUser;
            DatingFeature = new FacebookDatingFeature(LoggedInUser);
            GuessMyNameFeature = new FacebookGuessMyNameFeature(LoggedInUser) { Health = 6, Score = 0 };
        }

        public void ConnectToFacebookWithLastAccessToken(string i_LastAccessToken)
        {
            LastLoginResult = FacebookService.Connect(i_LastAccessToken);
            prepareFacebookApplication(LastLoginResult);
        }

        public LinkedList<User> MatchMe(string i_HomeTownFilter, User.eGender i_GenderFilter)
        {
            LinkedList<User> filteredFriendsList;

            filteredFriendsList = DatingFeature.GenerateFilteredFriendsList(i_HomeTownFilter, i_GenderFilter);

            return filteredFriendsList;
        }
    }
}

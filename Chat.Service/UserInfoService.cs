﻿using Chat.Model.DTO.UserInfo;
using Chat.Model.Entity.UserInfo;
using Chat.Model.Enum;
using Chat.Repository;
using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Service
{
    public class UserInfoService
    {
        private UserInfoRepository _userInfoRepository = new UserInfoRepository();
        public bool SetUserInfo(WXUserInfoRequest req)
        {
            try
            {
                var dto = new UserInfo()
                {
                    OpenId = req.openId,
                    NickName = req.nickName,
                    Gender = (GenderEnum)req.gender,
                    City = req.city,
                    Province = req.province,
                    Country=req.country,
                    Language = req.language,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                string headshotPath = Guid.NewGuid().ToString();
                string path = ConfigurationHelper.AppSettings["HeadPhoto"] + headshotPath + ConfigurationHelper.AppSettings["HeadshotFormat"];
                bool save = ImgHelper.Saveimages(req.avatarUrl, path);
                if (save)
                {
                    dto.HeadshotPath = headshotPath;
                }
                return _userInfoRepository.SetUserInfo(dto);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}

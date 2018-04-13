﻿using AutoMapper;
using Project.StoryDomain.Models;
using Project.UserProfileDomain.Models;
using Project.ViewModels;
using Project.ViewModels.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project {
    public static class AutoMapperConfig {

        public static void Configure() {
            Mapper.Initialize(cfg => {

                cfg.CreateMap<Interest, InterestVM>().ReverseMap();
                cfg.CreateMap<InterestVM, UserInterest>()
                    .ForMember(ivm => ivm.Interest, opt => opt.MapFrom(src => Mapper.Map<Interest>(src)))
                    .ReverseMap();

                cfg.CreateMap<Goal, GoalVM>().ReverseMap();
                cfg.CreateMap<GoalVM, UserGoal>()
                    .ForMember(gvm => gvm.Goal, opt => opt.MapFrom(src => Mapper.Map<Goal>(src)))
                    .ReverseMap();

                cfg.CreateMap<UserProfile, UserProfileVM>()
                    .ForMember(up => up.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(up => up.UserName, opt => opt.MapFrom(src => src.User.UserName))
                    .ForMember(up => up.Goals, opt => opt.MapFrom(src => Mapper.Map<List<GoalVM>>(src.Goals)))
                    .ForMember(up => up.Interests, opt => opt.MapFrom(src => Mapper.Map<List<InterestVM>>(src.Interests)))
                    .ReverseMap();

                cfg.CreateMap<UserProfile, UserProfileRefVM>();

                cfg.CreateMap<Story, ViewModels.Story.StoryVM>().ReverseMap();
                cfg.CreateMap<Comment, CommentVM>().ReverseMap();
                cfg.CreateMap<Hashtag, HashtagVM>().ReverseMap();
                cfg.CreateMap<Like, LikeVM>().ReverseMap();

            });
        }

    }
}
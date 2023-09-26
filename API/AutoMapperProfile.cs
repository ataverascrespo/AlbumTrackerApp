using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbumAPI.API.Migrations;
using AlbumAPI.DTOs.Profile;

/// <summary>
/// Configures mapping profiles for NuGet AutoMapper
/// </summary>
namespace AlbumAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Create a mapping profile for Album to AddAlbumDTO
            CreateMap<Album, GetAlbumDTO>();
            //Create a mapping profile for AddAlbumDTO to Album
            CreateMap<AddAlbumDTO, Album>();
            //Create a mapping profile for User to UserDTO
            CreateMap<User, UserDTO>();
            //Create a mapping profile for User to ProfileDTO
            CreateMap<User, ProfileDTO>()
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count));
            //Create a mapping profile for AlbumLikes to AlbumLikes DTO
            CreateMap<AlbumLike, AlbumLikesDTO>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User));
            //Create a mapping profile for AlbumLikes to AlbumLikes DTO
            CreateMap<UserFollowing, UserFollowingDTO>();
            //Create a mapping profile for userfollowing to userDTO
            CreateMap<UserFollowing, UserDTO>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.Follower!.ID))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Follower!.UserName))
                .ForMember(d => d.ImageURL, o => o.MapFrom(s => s.Follower!.ImageURL))
                .ForMember(d => d.ImageID, o => o.MapFrom(s => s.Follower!.ImageID))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Follower!.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Follower!.Followings.Count));
        }
    }
}
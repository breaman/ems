using AutoMapper;
using EMS.Domain.Models;
using EMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            // maps for TeamViewModel to Team
            CreateMap<TeamViewModel, Team>().ForMember(dest => dest.CompetitionLevelId, opt => opt.MapFrom(src => src.CompetitionLevel.Id))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Gender.Id))
                .ForMember(dest => dest.DivisionId, opt => opt.MapFrom(src => src.Division.Id))
                .ForMember(dest => dest.CompetitionLevel, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore())
                .ForMember(dest => dest.Division, opt => opt.Ignore())
                .ForMember(dest => dest.PromotionalCode, opt => opt.Ignore())
                .ForAllOtherMembers(opt => opt.Condition(r => r != null));

            // maps for team to teamviewmodel
            CreateMap<Gender, GenderViewModel>();
            CreateMap<Division, DivisionViewModel>();
            CreateMap<CompetitionLevel, CompetitionLevelViewModel>();
            CreateMap<Team, TeamViewModel>().ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Members))
                .ForMember(dest => dest.PromotionalCode, opt => opt.MapFrom(src => src.PromotionalCode.PromoCode));
            CreateMap<Team, GenderViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GenderId));
            CreateMap<Team, DivisionViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DivisionId));
            CreateMap<Team, CompetitionLevelViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompetitionLevelId));

            // maps for participantviewmodel to participant
            CreateMap<ParticipantViewModel, Participant>().ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address))
                //.ForMember(dest => dest.HeightFeet, opt => opt.MapFrom(src => src.HeightFeet.Id))
                //.ForMember(dest => dest.HeightInches, opt => opt.MapFrom(src => src.HeightInches.Id))
                //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Id))
                //.ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.NextGrade.Id))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.NextGrade))
                .ForMember(dest => dest.DayTimePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Zip))
                .ForMember(dest => dest.StateProvince, opt => opt.MapFrom(src => src.State))
                //.ForMember(dest => dest.StateProvince, opt => opt.MapFrom(src => src.State.Id))
                //.ForMember(dest => dest.TShirtSize, opt => opt.MapFrom(src => src.ShirtSize.Id))
                .ForMember(dest => dest.TShirtSize, opt => opt.MapFrom(src => src.ShirtSize));
            //.ForMember(dest => dest.PlayingExperience, opt => opt.MapFrom(src => src.PlayingExperience.Id))
            //.ForMember(dest => dest.PlayingFrequency, opt => opt.MapFrom(src => src.PlayingFrequency.Id));

            // maps for participant to participantviewmodel
            CreateMap<Participant, ParticipantViewModel>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.DayTimePhone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.PostalCode))
                //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.StateProvince))
                //.ForMember(dest => dest.ShirtSize, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ShirtSize, opt => opt.MapFrom(src => src.TShirtSize))
                //.ForMember(dest => dest.HeightFeet, opt=>opt.MapFrom(src => src))
                //.ForMember(dest => dest.HeightInches, opt => opt.MapFrom(src => src))
                //.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src))
                //.ForMember(dest => dest.NextGrade, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.NextGrade, opt => opt.MapFrom(src => src.Grade));
            //.ForMember(dest => dest.PlayingExperience, opt => opt.MapFrom(src => src))
            //.ForMember(dest => dest.PlayingFrequency, opt => opt.MapFrom(src => src));
            //CreateMap<Participant, GenderStringViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Gender));
            //CreateMap<Participant, HeightFeetViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HeightFeet));
            //CreateMap<Participant, HeightInchesViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HeightInches));
            //CreateMap<Participant, GradeViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Grade));
            //CreateMap<Participant, ShirtSizeViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TShirtSize));
            //CreateMap<Participant, StateViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StateProvince));
            //CreateMap<Participant, PlayingExperienceViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PlayingExperience));
            //CreateMap<Participant, PlayingFrequencyViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PlayingFrequency));

            CreateMap<Team, RegisteredTeamViewModel>().ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
                .ForMember(dest => dest.CompetitionLevelName, opt => opt.MapFrom(src => src.CompetitionLevel.Name));
            CreateMap<Participant, RegisteredParticipantViewModel>();
        }
    }
}

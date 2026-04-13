using AutoMapper;
using EcomQLDM.Data;
using EcomQLDM.ViewModels;

namespace EcomQLDM.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<RegisterVM, KhachHang>()
            //    .ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
            //    .ReverseMap();

            CreateMap<RegisterVM, KhachHang>();
            CreateMap<AddHangHoaVM, HangHoa>();
        }
    }
}

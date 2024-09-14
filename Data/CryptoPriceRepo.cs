using AutoMapper;
using GkoTradeService.Dtos;
using GkoTradeService.Enumerations;
using GkoTradeService.Extensions;
using GkoTradeService.Models;
using Microsoft.EntityFrameworkCore;

namespace GkoTradeService.Data
{
    public class CryptoPriceRepo : ICryptoPriceRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CryptoPriceRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CryptoPriceGetDto GetCryptoPriceById(int id)
        {
            var cryptoPrice = _context.CryptoPrices.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<CryptoPriceGetDto>(cryptoPrice);
        }

        public IEnumerable<CryptoPriceGetDto> GetCryptoPrices()
        {
            return _context.CryptoPrices
                .Select(x => new CryptoPriceGetDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Modified = x.Modified,
                    CryptoName = x.Crypto != 0 ? EnumExtension.GetEnumDescription(x.Crypto) : string.Empty,
                    Crypto = x.Crypto,
                    Price = x.Price,
                    Timestamp = x.Timestamp
                })
                .ToList();
        }

        public IEnumerable<CryptoPriceGetDto> GetCryptoPricesUserTimeRange(DateTime start, DateTime end)
        {
            return _context.CryptoPrices
                .Select(x => new CryptoPriceGetDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Modified = x.Modified,
                    CryptoName = x.Crypto != 0 ? EnumExtension.GetEnumDescription(x.Crypto) : string.Empty,
                    Crypto = x.Crypto,
                    Price = x.Price,
                    Timestamp = x.Timestamp
                })
                .Where(x => x.Timestamp.Date >= start.Date && x.Timestamp.Date <= end.Date)
                .ToList();
        }

        public async void AddCryptoPrice(CryptoPriceAddDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var newRecord = _mapper.Map<CryptoPriceAddDto, CryptoPrice>(model);
            _context.CryptoPrices.Add(newRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<CryptoPrice> GetLatestCryptoPrice(CryptosEnum crypto, DateTime requestedTime)
        {
            var cryptoPrice = await _context.CryptoPrices
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(x => x.Crypto == crypto
                    && x.Timestamp.Date == requestedTime.Date
                    && x.Timestamp.Hour == requestedTime.Hour);

            return cryptoPrice;
        }
    }
}
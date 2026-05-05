using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository donationRepository;

    public DonationService(IDonationRepository donationRepository)
    {
        this.donationRepository = donationRepository;
    }

    public List<GetDonationDto> GetDonations()
    {
        return donationRepository.GetDonations()
            .Select(d => new GetDonationDto(
                d.Id,
                d.Monto,
                d.Fecha,
                d.Estado
            ))
            .ToList();
    }

    public GetDonationDto? GetDonationById(int id)
    {
        var donation = donationRepository.GetDonationById(id);

        if (donation == null)
            return null;

        return new GetDonationDto(
            donation.Id,
            donation.Monto,
            donation.Fecha,
            donation.Estado
        );
    }

    public GetDonationDto CreateDonation(CreateDonationDto dto)
    {
        var donations = donationRepository.GetDonations();
        int newId = donations.Count > 0 ? donations.Max(d => d.Id) + 1 : 1;

        Donation newDonation = new Donation(
            newId,
            dto.Monto,
            DateTime.Now,
            dto.Estado
        );

        var createdDonation = donationRepository.CreateDonation(newDonation);

        return new GetDonationDto(
            createdDonation.Id,
            createdDonation.Monto,
            createdDonation.Fecha,
            createdDonation.Estado
        );
    }

    public GetDonationDto? UpdateDonation(int id, UpdateDonationDto dto)
    {
        var donation = donationRepository.GetDonationById(id);

        if (donation == null)
            return null;

        donation.Monto = dto.Monto;
        donation.Fecha = DateTime.Now;
        donation.Estado = dto.Estado;

        return new GetDonationDto(
            donation.Id,
            donation.Monto,
            donation.Fecha,
            donation.Estado
        );
    }

    public GetDonationDto? DeleteDonation(int id)
    {
        var donation = donationRepository.GetDonationById(id);

        if (donation == null)
            return null;

        var deletedDonation = new GetDonationDto(
            donation.Id,
            donation.Monto,
            donation.Fecha,
            donation.Estado
        );

        donationRepository.DeleteDonation(id);

        return deletedDonation;
    }
}
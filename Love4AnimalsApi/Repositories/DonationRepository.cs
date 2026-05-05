using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Repositories;

public class DonationRepository : IDonationRepository
{
    private List<Donation> Donations { get; set; }

    public DonationRepository()
    {
        Donations = new List<Donation>();

        Donations.Add(new Donation(1, 100, DateTime.Now, EstadoDonacionEnum.CONFIRMADA));
        Donations.Add(new Donation(2, 50, DateTime.Now, EstadoDonacionEnum.PENDIENTE));
    }

    public List<Donation> GetDonations()
    {
        return Donations;
    }

    public Donation? GetDonationById(int id)
    {
        return Donations.FirstOrDefault(d => d.Id == id);
    }

    public Donation CreateDonation(Donation donation)
    {
        Donations.Add(donation);
        return donation;
    }

    public bool UpdateDonation(int id, Donation donation)
    {
        var existingDonation = Donations.FirstOrDefault(d => d.Id == id);

        if (existingDonation == null)
            return false;

        existingDonation.Monto = donation.Monto;
        existingDonation.Fecha = donation.Fecha;
        existingDonation.Estado = donation.Estado;

        return true;
    }

    public bool DeleteDonation(int id)
    {
        var donation = Donations.FirstOrDefault(d => d.Id == id);

        if (donation == null)
            return false;

        Donations.Remove(donation);
        return true;
    }
}
using QuanLyCuaHangDoAnNhanh.DAO;
using QuanLyCuaHangDoAnNhanh.DTO;
using System.Collections.Generic;
using System.Data;

namespace QuanLyCuaHangDoAnNhanh.BLL
{
    public class ClientBLL
    {
        public List<Client> GetListClient()
        {
            return ClientDAO.Instance.GetListClient();
        }

        public DataTable GetClientTable()
        {
            return ClientDAO.Instance.GetClientTable();
        }

        public bool InsertClient(string name, string email, string phoneNumber, int bonusPoint = 0)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }
            return ClientDAO.Instance.InsertClient(name, email, phoneNumber, bonusPoint);
        }

        public bool UpdateClient(int id, string name, string email, string phoneNumber, int bonusPoint)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }
            return ClientDAO.Instance.UpdateClient(id, name, email, phoneNumber, bonusPoint);
        }

        public bool DeleteClient(int id)
        {
            return ClientDAO.Instance.DeleteClient(id);
        }

        public bool UpdateBonusPoint(int id, int bonusPoint)
        {
            return ClientDAO.Instance.UpdateBonusPoint(id, bonusPoint);
        }

        public bool AddBonusPoint(int id, int additionalPoints)
        {
            var client = GetListClient().Find(c => c.ID == id);
            if (client != null)
            {
                int newBonusPoint = client.BonusPoint + additionalPoints;
                return UpdateBonusPoint(id, newBonusPoint);
            }
            return false;
        }

        public bool GetClientByPhoneNumber(string phoneNumber, out Client client)
        {
            client = null;
            var clients = GetListClient();
            foreach (var c in clients)
            {
                if (c.PhoneNumber == phoneNumber)
                {
                    client = c;
                    return true;
                }
            }
            return false;
        }
    }
}

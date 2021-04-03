using System;
using Yelp.Api.Models;

namespace Seal.Models
{
    public class Model
    {
        public Model()
        {
        }
    }
    public class User
    {
        public int ID { get; set; }
        public int Level { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Request[] Requests { get; set; }
        public BusinessResponse Restaurant { get; set; }
        public string Token { get; set; }
    }
    public class Request
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RestaurantName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
    }
    public class Notification
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsRequest { get; set; }
        public bool Seen { get; set; }
    }
}

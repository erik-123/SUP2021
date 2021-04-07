using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using SUP2021.Models;
namespace SUP2021.Services
{
   public class FirebaseHelper
    {
             
            private readonly string ChildName = "TestProducts";

            readonly FirebaseClient firebase = new FirebaseClient("https://sup2021-c58ec-default-rtdb.europe-west1.firebasedatabase.app/");

            public async Task<List<Test>> GetAllPersons()
            {
                return (await firebase
                    .Child(ChildName)
                    .OnceAsync<Test>()).Select(item => new Test
                    {
                        ProductName = item.Object.ProductName,
                        ProductId = item.Object.ProductId,
                        
                    }).ToList();
            }

            public async Task AddPerson(string name)
            {
                await firebase
                    .Child(ChildName)
                    .PostAsync(new Test() { ProductId = Guid.NewGuid(), ProductName = name});
            }

            public async Task<Test> GetPerson(Guid productId)
            {
                var allPersons = await GetAllPersons();
                await firebase
                    .Child(ChildName)
                    .OnceAsync<Test>();
                return allPersons.FirstOrDefault(a => a.ProductId == productId);
            }

            public async Task<Test> GetPerson(string name)
            {
                var allPersons = await GetAllPersons();
                await firebase
                    .Child(ChildName)
                    .OnceAsync<Test>();
                return allPersons.FirstOrDefault(a => a.ProductName == name);
            }

            public async Task UpdatePerson(Guid productId, string name)
            {
                var toUpdatePerson = (await firebase
                    .Child(ChildName)
                    .OnceAsync<Test>()).FirstOrDefault(a => a.Object.ProductId == productId);

                await firebase
                    .Child(ChildName)
                    .Child(toUpdatePerson.Key)
                    .PutAsync(new Test() { ProductId = productId, ProductName = name});
            }

            public async Task DeletePerson(Guid productId)
            {
                var toDeletePerson = (await firebase
                    .Child(ChildName)
                    .OnceAsync<Test>()).FirstOrDefault(a => a.Object.ProductId == productId);
                    await firebase.Child(ChildName).Child(toDeletePerson.Key).DeleteAsync();
            }
        }
    }





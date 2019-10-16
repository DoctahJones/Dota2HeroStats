using Dota2HeroStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Dota2HeroStats.Services
{
    public class RoleStringToModelConverter
    {
        private IDataSource dataSource;


        public RoleStringToModelConverter(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<HashSet<Role>> CreateRoleList(IEnumerable<string> roleStrings)
        {
            var output = new HashSet<Role>();
            foreach (string s in roleStrings)
            {
                var roleToAdd = await dataSource.LookupRoleByName(s);
                if (roleToAdd != null)
                {
                    roleToAdd.EntityState = ModelEntityState.Unchanged;
                }
                else
                {
                    //create new role
                    roleToAdd = new Role
                    {
                        Name = s,
                        EntityState = ModelEntityState.Added
                    };
                }
                output.Add(roleToAdd);
            }
            return output;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Role : Entity
    {
        protected internal Role() { }

        public Role(string name)
        {
            SetName(name);
        }

        public string Name { get; private set; }

        public void SetName(string name)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(name, argumentName: "role name");
            ArgumentValidation.ThrowIfLongerThan(name, 255, argumentName: "role name");

            Name = name;
        }
    }
}

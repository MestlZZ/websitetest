using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using EasyPlan.Web.Controllers;
using System.Linq;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Web.Components;

namespace EasyPlan.Web.Tests
{
    [TestClass]
    public class BoardControllerTests
    { 
        private BoardController _controller;
        private IBoardRepository _boardRepository;
        private IMembershipProvider _membershipProvider;
        private IRoleProvider _roleProvider;

        [TestInitialize]
        public void Initialize()
        {
            _boardRepository = Substitute.For<IBoardRepository>();
            _membershipProvider = Substitute.For<IMembershipProvider>();
            _roleProvider = Substitute.For<IRoleProvider>();

            _controller = new BoardController(_boardRepository, _membershipProvider, _roleProvider);
        }
    }
}

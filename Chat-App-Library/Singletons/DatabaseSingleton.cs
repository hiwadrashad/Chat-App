﻿using Chat_App_Library.Interfaces;
using Chat_App_Library.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Singletons
{

    public class DatabaseSingleton : IDatabaseSingleton
    {
        private static IRepository _repository;
        private DatabaseSingleton()
        {

        }

        private static DatabaseSingleton _databaseSingleton;

        public static DatabaseSingleton GetSingleton()
        {
            if (_databaseSingleton == null)
            {
                _databaseSingleton = new DatabaseSingleton();
            }
            return _databaseSingleton;
        }

        public void SetRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IRepository GetRepository()
        {
            return _repository;
        }

        public void SetMoqRepository(IRepository repository)
        {
            _repository = repository;
        }
    }
}
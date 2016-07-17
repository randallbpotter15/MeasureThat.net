﻿using System;
using BenchmarkLab.Models;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Linq;

namespace BenchmarkLab.Data.Dao
{
    public interface IBenchmarksRepository : IEntityRepository<NewBenchmarkModel, int>
    {
        NewBenchmarkModel FindBenchmark(int benchmarkId, int version);
    }

    public class MockBenchmarksRepository : IBenchmarksRepository
    {
        private static int benchmarkId = 0;

        private List<NewBenchmarkModel> m_repository = new List<NewBenchmarkModel>()
        {
            new NewBenchmarkModel()
            {
                BenchmarkName = "Mock Benchmark 1",
                BenchmarkVersion = 1,
                Description = "Mock Description",
                Id = benchmarkId++
            }
        };
               

        public void Add([NotNull] NewBenchmarkModel entity)
        {
            entity.Id = benchmarkId++;
            this.m_repository.Add(entity);
        }

        public void Delete(NewBenchmarkModel entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public NewBenchmarkModel FindBenchmark(int benchmarkId, int version)
        {
            var result = this.m_repository.FirstOrDefault(t => t.Id == benchmarkId && t.BenchmarkVersion == version);
            return result;
        }

        public NewBenchmarkModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewBenchmarkModel> ListAll()
        {
            return this.m_repository.AsReadOnly();
        }
    }
}
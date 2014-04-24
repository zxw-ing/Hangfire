﻿using System;
using System.Collections.Generic;
using HangFire.Common;
using HangFire.States;
using Xunit;

namespace HangFire.Core.Tests.States
{
    public class ScheduledStateFacts
    {
        [Fact]
        public void StateName_IsCorrect()
        {
            var state = new ScheduledState(DateTime.UtcNow);
            Assert.Equal(ScheduledState.StateName, state.Name);
        }

        [Fact]
        public void Ctor_SetsTheCorrectData_WhenDateIsPassed()
        {
            var date = new DateTime(2012, 12, 12);
            var state = new ScheduledState(date);
            Assert.Equal(date, state.EnqueueAt);
        }

        [Fact]
        public void Ctor_SetsTheCorrectDate_WhenTimeSpanIsPassed()
        {
            var state = new ScheduledState(TimeSpan.FromDays(1));
            Assert.True(DateTime.UtcNow.AddDays(1).AddMinutes(-1) < state.EnqueueAt);
            Assert.True(state.EnqueueAt < DateTime.UtcNow.AddDays(1).AddMinutes(1));
        }

        [Fact]
        public void SerializeData_ReturnsCorrectData()
        {
            var state = new ScheduledState(new DateTime(2012, 12, 12));

            var data = state.SerializeData();

            Assert.Equal(JobHelper.ToStringTimestamp(state.EnqueueAt), data["EnqueueAt"]);
            Assert.Equal(JobHelper.ToStringTimestamp(state.ScheduledAt), data["ScheduledAt"]);
        }
    }
}

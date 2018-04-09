﻿using Project.Core.Models;

namespace Project.UserProfileDomain.Models {
    public class Goal : ObjectWithState {

        public int Id { get; set; }

        public string Title { get; set; }

    }
}
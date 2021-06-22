/**
 * IFeaturePriority.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.Features {

    public interface IFeaturePriority {

        Task<bool> HaveFeaturePriorityAsync();
    }
}

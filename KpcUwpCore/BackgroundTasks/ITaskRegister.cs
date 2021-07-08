/**
 * ITaskRegister.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.Collections.Generic;
using Windows.ApplicationModel.Background;


namespace KanoComputing.KpcUwpCore.BackgroundTasks {

    public interface ITaskRegister {

        IBackgroundTaskRegistration RegisterBackgroundTask<T>(
            IBackgroundTrigger trigger,
            IReadOnlyList<IBackgroundCondition> conditions = null);
    }
}

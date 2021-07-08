/**
 * TaskRegister.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel.Background;


namespace KanoComputing.KpcUwpCore.BackgroundTasks {

    public class TaskRegister : ITaskRegister {

        /// <summary>
        /// Register an out of process background task for the app.
        /// </summary>
        /// <typeparam name="T">The class implementing the background task.</typeparam>
        /// <param name="trigger">A background trigger to invoke the task.</param>
        /// <param name="conditions">Additional optional triggering conditions.</param>
        /// <returns></returns>
        public IBackgroundTaskRegistration RegisterBackgroundTask<T>(
                IBackgroundTrigger trigger,
                IReadOnlyList<IBackgroundCondition> conditions = null) {

            string taskName = typeof(T).Name;
            string taskEntry = typeof(T).FullName;

            // Check for existing registrations of this background task.
            foreach (var pair in BackgroundTaskRegistration.AllTasks) {
                if (pair.Value.Name == taskName) {
                    Debug.WriteLine($"{this.GetType()}: RegisterBackgroundTask: " +
                        $"Task '{taskName}' is already registered");
                    return pair.Value;
                }
            }

            // Register the background task.
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder {
                Name = taskName,
                TaskEntryPoint = taskEntry
            };
            builder.SetTrigger(trigger);

            // Additional triggering conditions are not always needed.
            if (conditions != null) {
                foreach (var condition in conditions) {
                    builder.AddCondition(condition);
                }
            }

            IBackgroundTaskRegistration task = null;
            try {
                task = builder.Register();
                if (task != null) {
                    Debug.WriteLine($"{this.GetType()}: RegisterBackgroundTask: " +
                        $"Registered background task '{taskName}'");
                } else {
                    Debug.WriteLine($"{this.GetType()}: RegisterBackgroundTask: " +
                        $"Failed to register background task '{taskName}'");
                }
            } catch (Exception e) {
                Debug.WriteLine($"{this.GetType()}: RegisterBackgroundTask: " +
                     $"Failed to register background task: {e}");
            }
            return task;
        }
    }
}

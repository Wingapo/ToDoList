import React from 'react';
import { useSelector } from "react-redux";
import {ACTIVE, COMPLETED} from "../statusTypes";
import Task from "./Task";

function TaskList() {
    const { tasks } = useSelector(state => state.Tasks);

    const active = tasks.filter((task) => task.status === ACTIVE);
    const completed = tasks.filter((task) => task.status === COMPLETED);

    return (
        <>
            <div className="card mb-2">
                <div className="card-header">
                    <div className="badge rounded-pill bg-warning fs-6">Active</div>
                </div>
                {active.map(task => <Task key={task.id} task={task}/>)}
            </div>
            <div className="card">
                <div className="card-header">
                    <div className="badge rounded-pill bg-success fs-6">Completed</div>
                </div>
                {completed.map(task => <Task key={task.id} task={task}/>)}
            </div>
        </>
    );
}

export default TaskList;
import React from 'react';
import { useDispatch, useSelector } from "react-redux";
import {completeTask, deleteTask} from "../redux/actions/taskActions";
import {COMPLETED} from "../statusTypes";

function Task({ task }) {
    const dispatch = useDispatch();

    const { categories } = useSelector(state => state.Categories);

    const selectedCategories = [];

    task.categoryIds.forEach(id => {
        const category = categories.find(category => category.id === parseInt(id));
        if (category != null) {
            selectedCategories.push(category);
        }
    })

    console.log(task.categoryIds);

    return (
        <div className="card-body">
            <div className="card">
                <div className="card-header">
                    <div className="btn-group float-end">
                        {task.status !== COMPLETED &&
                            (<button className="btn btn-success"
                                     onClick={() => dispatch(completeTask(task.id))}>✓</button>)}
                        <button className="btn btn-danger"
                                onClick={() => dispatch(deleteTask(task.id))}>⨯</button>
                    </div>
                    <div className="card-title fs-5">{task.name}</div>
                    <div className="card-text">{task.deadline}</div>
                </div>
                <div className="card-body">
                    {selectedCategories.length > 0 &&
                        <ul className="d-flex flex-wrap list-unstyled gap-2">
                            {selectedCategories.map(category => {
                                return <li className="border rounded bg-light px-2 py-1"
                                           key={category.id}>{category.name}</li>
                            })}
                        </ul>}
                    <div>{task.details}</div>
                </div>
            </div>
        </div>
    );
}

export default Task;
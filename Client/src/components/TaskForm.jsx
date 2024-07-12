import React, {useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {addTask} from "../redux/actions/taskActions";
import {ACTIVE} from "../statusTypes";

function TaskForm() {
    const dispatch = useDispatch();
    const { categories } = useSelector(state => state.Categories);

    const [name, setName] = useState('');
    const [details, setDetails] = useState('');
    const [categoryIds, setCategoryIds] = useState([]);
    const [deadline, setDeadline] = useState('');

    const handleSubmit = e => {
        e.preventDefault();

        const newTask = {
            name,
            details,
            status: ACTIVE,
            categoryIds,
            deadline
        }
        dispatch(addTask(newTask));

        setName('');
        setDetails('');
        setCategoryIds([]);
        setDeadline('');
    }

    return (
        <div className="card col-6">
            <div className="card-title">
                <h4 className="ms-1">Add Task</h4>
            </div>
            <form className="card-body" onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label htmlFor="name" className="form-label">Name</label>
                    <input className="form-control"
                           type="text"
                           id="name"
                           value={name}
                           onChange={(e) => setName(e.target.value)}/>
                </div>
                <div className="mb-3">
                    <label htmlFor="details" className="form-label">Details</label>
                    <textarea className="form-control"
                              id="details"
                              value={details}
                              onChange={(e) => setDetails(e.target.value)}></textarea>
                </div>
                <div className="mb-3">
                    <label htmlFor="categories" className="form-label">Categories</label>
                    <select className="form-select"
                            id="categories"
                            multiple
                            value={categoryIds}
                            onChange={(e) => {
                                const options = [...e.target.selectedOptions];
                                setCategoryIds(options.map(option => option.value));
                            }}>
                        {categories.map(category =>
                            <option key={category.id} value={category.id}>{category.name}</option>)}
                    </select>
                </div>
                <div className="mb-3">
                    <label htmlFor="details" className="form-label">Deadline</label>
                    <input className="form-control"
                           type="date"
                           id="deadline"
                           value={deadline}
                           onChange={(e) => setDeadline(e.target.value)}/>
                </div>
                <button className="btn btn-success float-end" type="submit">Add</button>
            </form>
        </div>
    );
}

export default TaskForm;
import {ADD_TASK, COMPLETE_TASK, DELETE_TASK} from "./taskActionTypes";

let nextId = 1;

export const addTask = task => ({
    type: ADD_TASK,
    payload: { task: {...task, id: nextId++} }
});

export const completeTask = id => ({
    type: COMPLETE_TASK,
    payload: { id }
});

export const deleteTask = id => ({
    type: DELETE_TASK,
    payload: { id }
});
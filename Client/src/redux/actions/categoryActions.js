import {ADD_CATEGORY, DELETE_CATEGORY} from "./categoryActionsTypes";

let nextId = 1;
export const addCategory = category => ({
    type: ADD_CATEGORY,
    payload: { category: {...category, id: nextId++} }
});
export const deleteCategory = id => ({
    type: DELETE_CATEGORY,
    payload: { id }
})
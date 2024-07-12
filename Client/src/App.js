import TaskForm from "./components/TaskForm";
import TaskList from "./components/TaskList";
import CategoryForm from "./components/CategoryForm";
import CategoryList from "./components/CategoryList";
import './App.css'

function App() {
  return (
    <div className="container mt-2">
        <div className="d-flex gap-2 mb-2">
            <TaskForm/>
            <div className="col d-flex flex-column ">
                <CategoryForm/>
                <CategoryList/>
            </div>
        </div>
        <TaskList/>
    </div>
  );
}

export default App;

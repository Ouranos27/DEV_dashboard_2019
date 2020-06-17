import React, {Component} from 'react';
import authService from './api-authorization/AuthorizeService';
import Dashboard from "./Dashboard";

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isAuthenticated: false,
            userName: null
        }
    }
    
    componentWillMount() {
        this.populate();
    }

    static displayName = Home.name;
    
    /**_isAuthenticated = async() => {
        return await authService.isAuthenticated();
    };*/
    
    async populate() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()]);
        this.setState({
            isAuthenticated,
            userName: user && user.name 
        });
    }

    render() {
        //const isAuthenticated = this._isAuthenticated();
        const {isAuthenticated, userName} = this.state;
        let content;
        if (!isAuthenticated) {
            content = <div>
                <h1>Hello, world! {userName}</h1>
                <p>Welcome to your Dashboard, built with:</p>
                <ul>
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a
                        href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform
                        server-side code
                    </li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='https://material-ui.com/'>Material-UI</a> for layout and styling</li>
                </ul>
            </div>
        } else {
            content = <Dashboard/>
        }
        return (
            <div>{content}</div>
        );
    }
}

import React, { Component, Fragment } from "react";
import { render } from "react-dom";

class ReactExample extends Component {

	state = {
		newItemText: "",
		items: [],
		...window.initialData
	}

	handleNewItemTextChanged = (e) => {
		const newItemText = e.currentTarget.value;

		this.setState({ newItemText });
	}

	handleNewItemFormSubmit = (e) => {
		e.preventDefault();

		const newItemText = "";
		const items = [...this.state.items, { name: this.state.newItemText, isSelected: false }];

		this.setState({
			newItemText,
			items
		});
	}

	handleItemIsSelctedChange = (item, e) => {
		const items = this.state.items.map(i => i === item ? { ...i, isSelected: e.currentTarget.checked } : i);
		this.setState({ items });
	}

	handleRemoveItemClick = (item, e) => {
		e.preventDefault();
		const items = this.state.items.filter(i => i !== item);
		this.setState({ items });
	}

	render() {
		const { title, newItemText, items } = this.state;

		return (
			<div className="container">
				<h1>{title}</h1>

				<div className="row">
					<div className="col-md-6">
						<form onSubmit={this.handleNewItemFormSubmit}>
							<div className="form-group">
								<input className="form-control input-lg" type="text" placeholder="Add items..." value={newItemText}
									onChange={this.handleNewItemTextChanged} />
							</div>
						</form>

						<hr />

						{items.map((item, i) =>
							<Fragment key={i}>
								<div className="row">
									<div className="col-md-10">
										<div className="checkbox">
											<label>
												<input type="checkbox" checked={item.isSelected} onChange={this.handleItemIsSelctedChange.bind(this, item)} />
												{item.name}
											</label>
										</div>
									</div>
									<div className="col-md-2">
										<a href="#" onClick={this.handleRemoveItemClick.bind(this, item)}>Remove</a>
									</div>
								</div>

								<hr />
							</Fragment>
						)}
					</div>
				</div>
			</div>
		);
	}
}

render(<ReactExample />, document.getElementById("root"));

# Project Structure

<p>
	docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
</p>




	docs
	src
		Core
			Application
				DependencyInjection			
				common
					behaviours
					exception
					interfaces
					mappings
					models
					viewModels
						PaginatedItemsViewModel
				(Item)
					Commands
						Create
							Command
							Handler
							Validator
						Delete
							Command
							Handler
							Validator
						Update
							Command
							Handler
							Validator
					Queries
						GetList
							Command
							Handler
							Validator
						GetDetail
							Command
							Handler
							Validator
			Domain
				common
					AuditableEntity
					DomainEvent
					ValueObject
				entities
				enums
				events
				exceptions
				valueObjects
		Infrastructure
			Persistence
				DependencyInjection.cs
				configuration
				migrations
				databases
					projectContext
		Presentation
			WebApi
	tests
	docker-compose